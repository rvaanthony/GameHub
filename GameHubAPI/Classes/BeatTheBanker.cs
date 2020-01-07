using GameHub.Classes;
using GameHub.Models;
using GameHub.Models.BTB;
using GameHubAPI.Interfaces;
using GameHubAPI.Models.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameHubAPI.Classes
{
    public class BeatTheBanker
    {
        #region Properties

        private readonly IDataContextProvider _dataContext;
        private readonly IApiHelperTrace _apiTrace;
        private readonly GameHubContext _dbContext;
        private readonly ITracker _tracker;
        private readonly ILog _logger;
        public UserContextModel _userInfo { get; set; }

        public BeatTheBanker(IApiHelperTrace apiTrace, IDataContextProvider dataContext, ILog logger, ITracker tracker)
        {
            _dbContext = dataContext.GetGameHubContext();
            _dataContext = dataContext;
            _apiTrace = apiTrace;
            _tracker = tracker;
            _logger = logger;
        }

        #endregion

        #region Public Functions

        public BTBGameModel InitializeNewGame()
        {
            try
            {
                if(_userInfo == null)
                    return new BTBGameModel() { ErrorFlag = true, Message = "No user context" };

                var gameInfo = InitializeGame();
                return gameInfo;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs InitializeNewGame()");
                return new BTBGameModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        public BTBGameModel GetGameInfo(string gameId)
        {
            try
            {
                if (_userInfo == null)
                    return new BTBGameModel() { ErrorFlag = true, Message = "No user context" };

                var gameInfo = _dbContext.TblBtbgame.Where(a => a.Guid == gameId).Select(a => new BTBGameModel()
                {
                    Id = a.Id,
                    GameGuid = a.Guid,
                    Created = a.Created
                }).FirstOrDefault();

                if(gameInfo == null)
                    return new BTBGameModel() { ErrorFlag = true, Message = "No game found" };

                var cases = GetGameCases(gameInfo.Id);
                if(cases.ErrorFlag)
                    return new BTBGameModel() { ErrorFlag = true, Message = "No cases found" };

                gameInfo.Cases = cases.Cases;

                return gameInfo;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs GetGameInfo({gameId})");
                return new BTBGameModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        public BTBCaseAmountListModel GetGameBoard()
        {
            try
            {
                if (_userInfo == null)
                    return new BTBCaseAmountListModel() { ErrorFlag = true, Message = "No user context" };

                var amounts = _dbContext.TblBtbcaseAmount.Where(a => a.Active).Select(a => new BTBCaseAmountModel()
                {
                    Amount = a.Amount,
                    Id = a.Id
                }).ToList();
                return new BTBCaseAmountListModel() { List = amounts };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs GetGameBoard()");
                return new BTBCaseAmountListModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        public BTBBankerOffer OpenGameCase(BTBGameCaseModel gameCase)
        {
            try
            {
                if (_userInfo == null)
                    return new BTBBankerOffer() { ErrorFlag = true, Message = "No user context" };

                var results = StartGame(gameCase.GameGuid);
                if (results.ErrorFlag)
                    return new BTBBankerOffer() { ErrorFlag = true, Message = results.Message };

                var foundCase = _dbContext.TblBtbgameCase.FirstOrDefault(a => a.Guid == gameCase.Guid && a.Game.Guid == gameCase.GameGuid);
                if (foundCase == null) return null;

                results = InsertOpenCaseRecord(foundCase.GameId, foundCase.Id);
                if (results.ErrorFlag)
                    return new BTBBankerOffer() { ErrorFlag = true, Message = results.Message };

                foundCase.Modified = DateTimeOffset.Now;
                foundCase.StatusId = (int) Enums.BTBStatusType.Opened;

                _dbContext.SaveChanges();

                _tracker.TrackAction(Enums.TrackingActionType.BTBCaseOpened, Enums.TableEntity.tblBTBGameCase, foundCase.Id.ToString(), _userInfo.UserId, foundCase.Guid);

                var remainingCases = GameRemainingCases(foundCase.GameId);
                if (remainingCases.ErrorFlag)
                    return new BTBBankerOffer() { ErrorFlag = true, Message = remainingCases.Message };

                if(gameCase.GameOver) return new BTBBankerOffer() { NewOffer = false };

                if (remainingCases.Cases.Count != 14 && remainingCases.Cases.Count != 9 &&
                    remainingCases.Cases.Count != 5 &&
                    remainingCases.Cases.Count != 1) return new BTBBankerOffer() {NewOffer = false};

                var bankOffer = BankerOffer(remainingCases);
                results = LogBankOffer(bankOffer, foundCase.GameId);

                if (results.ErrorFlag)
                    return new BTBBankerOffer() {ErrorFlag = true, Message = results.Message};

                bankOffer.Id = int.Parse(results.Data);
                bankOffer.GameId = foundCase.GameId;

                return bankOffer;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs OpenGameCase({JsonConvert.SerializeObject(gameCase)})");
                return new BTBBankerOffer() { ErrorFlag = true, Message = exception.Message };
            }
        }

        public async Task<ResultModel> UpdateBankOffer(BTBBankerOffer offer, Enums.BTBStatusType status)
        {
            try
            {
                if (_userInfo == null)
                    return new ResultModel() { ErrorFlag = true, Message = "No user context" };

                var foundRecord = _dbContext.TblBtbgameBankerOffer.FirstOrDefault(a =>
                    a.Id == offer.Id && a.StatusId == (int)Enums.BTBStatusType.Pending);

                if (foundRecord == null)
                    return new ResultModel() { ErrorFlag = true, Message = "No record found" }; ;

                foundRecord.Modified = DateTimeOffset.Now;
                foundRecord.StatusId = (int)status;

                _dbContext.SaveChanges();


                if (status == Enums.BTBStatusType.Accepted)
                {
                    _tracker.TrackAction(Enums.TrackingActionType.BTBAcceptedBankOffer, Enums.TableEntity.tblBTBGameBankerOffer, foundRecord.Id.ToString(), _userInfo.UserId, offer.Amount.ToString(CultureInfo.InvariantCulture));

                    return await EndGame(offer.GameId, Enums.BTBGameResult.BankOfferAccepted, Convert.ToInt32(offer.Amount), foundRecord.Id.ToString());
                }

                _tracker.TrackAction(Enums.TrackingActionType.BTBDeclinedBankOffer, Enums.TableEntity.tblBTBGameBankerOffer, foundRecord.Id.ToString(), _userInfo.UserId, offer.Amount.ToString(CultureInfo.InvariantCulture));

                return new ResultModel() { Success = true };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs UpdateBankOffer({JsonConvert.SerializeObject(offer)}, {status})");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        public ResultModel SwapCase()
        {
            try
            {
                if (_userInfo == null)
                    return new ResultModel() { ErrorFlag = true, Message = "No user context" };

                return new ResultModel() { Success = true };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs SwapCase()");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        public BTBGameCaseModel SwapFinalCase(string gameGuid)
        {
            try
            {
                if (_userInfo == null)
                    return new BTBGameCaseModel() { ErrorFlag = true, Message = "No user context" };

                var foundCase = _dbContext.TblBtbgameCase
                    .FirstOrDefault(a => a.Game.Guid == gameGuid && a.StatusId == (int)Enums.BTBStatusType.Active);

                if (foundCase == null)
                    return new BTBGameCaseModel() { ErrorFlag = true, Message = "No case found" };

                var chosenCase = _dbContext.TblBtbgameChosenCase
                    .FirstOrDefault(a => a.GameCase.GameId == foundCase.GameId && a.StatusId == (int)Enums.BTBStatusType.Active);

                if (chosenCase == null)
                    return new BTBGameCaseModel() { ErrorFlag = true, Message = "No case found" };

                chosenCase.StatusId = (int)Enums.BTBStatusType.Swapped;
                chosenCase.Modified = DateTimeOffset.Now;

                _dbContext.SaveChanges();

                return InsertNewOpenCaseRecord(foundCase.GameId, foundCase.Id);
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs SwapFinalCase({gameGuid})");
                return new BTBGameCaseModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        public BTBGameCaseModel CaseData(string caseGuid)
        {
            try
            {
                if (_userInfo == null)
                    return new BTBGameCaseModel() { ErrorFlag = true, Message = "No user context" };

                var foundCase = _dbContext.TblBtbgameCase.Where(a => a.Guid == caseGuid).Select(a => new BTBGameCaseModel()
                {
                    Id = a.Id,
                    Amount = a.CaseAmount.Amount
                }).FirstOrDefault();

                return foundCase;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs CaseData({caseGuid})");
                return new BTBGameCaseModel() {ErrorFlag = true, Message = exception.Message};
            }
        }

        #endregion


        #region Private Functions

        private BTBGameModel InitializeGame()
        {
            try
            {
                var newRecord = new TblBtbgame()
                {
                    Guid = Guid.NewGuid().ToString(),
                    UserId = _dbContext.TblUser.Where(a => a.Guid == _userInfo.UserId).Select(a => a.Id).FirstOrDefault(),
                    Created = DateTimeOffset.Now,
                    StatusId = (int)Enums.BTBStatusType.New
                };

                _dbContext.TblBtbgame.Add(newRecord);
                _dbContext.SaveChanges();

                var cases = AddCasesToGame(newRecord.Id);
                if (cases.ErrorFlag)
                    return new BTBGameModel() { ErrorFlag = true, Message = cases.Message };

                return new BTBGameModel()
                {
                    Id = newRecord.Id,
                    GameGuid = newRecord.Guid,
                    Cases = cases.Cases,
                    Created = newRecord.Created
                };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs InitializeGame()");
                return new BTBGameModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private ResultModel StartGame(string gameGuid)
        {
            try
            {
                var foundGame = _dbContext.TblBtbgame.FirstOrDefault(a => a.Guid == gameGuid && a.StatusId == (int)Enums.BTBStatusType.New);
                if (foundGame != null)
                {
                    foundGame.StartTime = DateTimeOffset.Now;
                    foundGame.StatusId = (int)Enums.BTBStatusType.Started;

                    _dbContext.SaveChanges();
                }
                return new ResultModel() { Success = true };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs StartGame({gameGuid})");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private BTBGameCasesModel AddCasesToGame(int gameId)
        {
            try
            {
                var result = GetCaseAmount();
                if(result.ErrorFlag)
                    return new BTBGameCasesModel() { ErrorFlag = true, Message = result.Message };

                //Just want to make sure we are only creating 20 cases
                var amounts = result.List.Take(20);
                var caseNumber = 1;
                var cases = new BTBGameCasesModel() { Cases = new List<BTBGameCaseModel>() };
                foreach (var amount in amounts.OrderBy(elem => Guid.NewGuid()))
                {
                    var newRecord = new TblBtbgameCase()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        GameId = gameId,
                        CaseAmountId = amount.Id,
                        CaseNumber = caseNumber,
                        StatusId = (int)Enums.BTBStatusType.Active,
                        Created = DateTimeOffset.Now
                    };

                    _dbContext.TblBtbgameCase.Add(newRecord);
                    _dbContext.SaveChanges();

                    cases.Cases.Add(new BTBGameCaseModel()
                    {
                        Id = newRecord.Id,
                        GameId = newRecord.GameId,
                        CaseAmountId = newRecord.CaseAmountId,
                        CaseNumber = newRecord.CaseNumber
                    });

                    caseNumber++;
                }

                return cases;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs AddCasesToGame({gameId})");
                return new BTBGameCasesModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private BTBCaseAmountListModel GetCaseAmount()
        {
            try
            {
                var amounts = _dbContext.TblBtbcaseAmount.Where(a => a.Active).Select(a => new BTBCaseAmountModel()
                {
                    Id = a.Id,
                    Amount = a.Amount,
                    Active = a.Active
                }).ToList();

                return new BTBCaseAmountListModel() { List = amounts };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs GetCaseAmount()");
                return new BTBCaseAmountListModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private BTBGameCasesModel GetGameCases(int gameId)
        {
            try
            {
                var cases = _dbContext.TblBtbgameCase.Where(a => a.GameId == gameId).Select(a => new BTBGameCaseModel()
                    {
                        Guid = a.Guid,
                        GameId = a.GameId,
                        CaseAmountId = a.CaseAmountId,
                        CaseNumber = a.CaseNumber,
                        Amount = a.CaseAmount.Amount
                    }).ToList();

                return new BTBGameCasesModel() { Cases = cases };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs GetGameCases({gameId})");
                return new BTBGameCasesModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private ResultModel InsertOpenCaseRecord(int gameId, int caseId)
        {
            try
            {
                var checkForCase = _dbContext.TblBtbgameChosenCase.Any(a => a.GameCase.GameId == gameId && a.StatusId == (int)Enums.BTBStatusType.Active);

                if(checkForCase)
                    return new ResultModel() { Success = true };

                var newRecord = new TblBtbgameChosenCase()
                {
                    StatusId = (int)Enums.BTBStatusType.Active,
                    Created = DateTimeOffset.Now,
                    GameCaseId = caseId
                };

                _dbContext.TblBtbgameChosenCase.Add(newRecord);
                _dbContext.SaveChanges();

                return UpdateGameCaseStatus(caseId, Enums.BTBStatusType.Selected);
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs InsertOpenCaseRecord({gameId}, {caseId})");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private ResultModel UpdateGameCaseStatus(int caseId, Enums.BTBStatusType status)
        {
            try
            {
                var foundRecord = _dbContext.TblBtbgameCase.FirstOrDefault(a => a.Id == caseId);

                if(foundRecord == null)
                    return new ResultModel() { ErrorFlag = true, Message = "No record found" };

                foundRecord.Modified = DateTimeOffset.Now;
                foundRecord.StatusId = (int) status;

                _dbContext.SaveChanges();

                return new ResultModel() { Success = true };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs UpdateGameCaseStatus({caseId}, {status})");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private BTBGameCaseModel InsertNewOpenCaseRecord(int gameId, int caseId)
        {
            try
            {
                var newRecord = new TblBtbgameChosenCase()
                {
                    StatusId = (int)Enums.BTBStatusType.Active,
                    Created = DateTimeOffset.Now,
                    GameCaseId = caseId
                };

                _dbContext.TblBtbgameChosenCase.Add(newRecord);
                _dbContext.SaveChanges();

                var caseInfo = _dbContext.TblBtbgameChosenCase.Where(a => a.Id == newRecord.Id)
                    .Select(a => new BTBGameCaseModel()
                    {
                        Id = a.Id,
                       CaseNumber = a.GameCase.CaseNumber,
                       Guid = a.GameCase.Guid
                    }).FirstOrDefault();

                return caseInfo;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs InsertNewOpenCaseRecord({gameId}, {caseId})");
                return new BTBGameCaseModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private BTBGameCasesModel GameRemainingCases(int gameId)
        {
            try
            {
                var remainingCases = _dbContext.TblBtbgameCase.Where(a => a.GameId == gameId && a.StatusId == (int)Enums.BTBStatusType.Active).Select(a => new BTBGameCaseModel()
                {
                    Id = a.Id,
                    CaseAmountId = a.CaseAmountId,
                    CaseNumber = a.CaseNumber,
                    Amount = a.CaseAmount.Amount
                }).ToList();

                return new BTBGameCasesModel() { Cases = remainingCases };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs GameRemainingCases({gameId})");
                return new BTBGameCasesModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private BTBBankerOffer BankerOffer(BTBGameCasesModel remainingCases)
        {
            try
            {
                var average = remainingCases.Cases.Select(a => a.Amount).Average();
                var e = average * .75;
                var e2 = Math.Pow(e, 2);
                var c = remainingCases.Cases.Count;
                var c2 = Math.Pow(c, 2);
                var m = remainingCases.Cases.OrderByDescending(a => a.Amount).Select(a => a.Amount).FirstOrDefault();
                var offer = 12275.30 + (0.748 * e) - (-2714.74 * c) - (-0.040 * m) + (.0000006986 * e2) + (32.623 * c2);

                return new BTBBankerOffer() { NewOffer = true, Amount = offer };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs BankerOffer({JsonConvert.SerializeObject(remainingCases)})");
                return new BTBBankerOffer() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private ResultModel LogBankOffer(BTBBankerOffer offer, int gameId)
        {
            try
            {
                var newRecord = new TblBtbgameBankerOffer()
                {
                    GameId = gameId,
                    OfferAmount = Convert.ToInt32(offer.Amount),
                    StatusId = (int)Enums.BTBStatusType.Pending,
                    Created = DateTimeOffset.Now
                };

                _dbContext.TblBtbgameBankerOffer.Add(newRecord);
                _dbContext.SaveChanges();

                _tracker.TrackAction(Enums.TrackingActionType.BTBAcceptedBankOffer, Enums.TableEntity.tblBTBGameBankerOffer, newRecord.Id.ToString(), _userInfo.UserId, offer.Amount.ToString(CultureInfo.InvariantCulture));

                return new ResultModel() { Success = true, Data = newRecord.Id.ToString() };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs LogBankOffer({JsonConvert.SerializeObject(offer)}, {gameId})");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private ResultModel LogGameResults(int gameId, Enums.BTBGameResult gameResult, int amountWon, string sourceId)
        {
            try
            {
                var entityTable = Enums.TableEntity.tblBTBGame;
                switch (gameResult)
                {
                    case Enums.BTBGameResult.BankOfferAccepted:
                        entityTable = Enums.TableEntity.tblBTBGameBankerOffer;
                        break;
                    case Enums.BTBGameResult.CaseKept:
                        entityTable = Enums.TableEntity.tblBTBGameChoosenCase;
                        break;
                }
                var newRecord = new TblBtbgameResult()
                {
                    GameId = gameId,
                    ResultId = Convert.ToByte((int)gameResult),
                    AmountWon = amountWon,
                    TableEntityId = (int)entityTable,
                    SourceId = sourceId,
                    Created = DateTimeOffset.Now
                };

                _dbContext.TblBtbgameResult.Add(newRecord);
                _dbContext.SaveChanges();

                return new ResultModel() { Success = true };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs LogGameResults({gameId}, {gameResult}, {amountWon}, {sourceId})");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private async Task<ResultModel> EndGame(int gameId, Enums.BTBGameResult gameResult, int amountWon, string sourceId)
        {
            try
            {
                var results = LogGameResults(gameId, gameResult, amountWon, sourceId);
                if (results.ErrorFlag)
                    return results;

                var foundGame = _dbContext.TblBtbgame.FirstOrDefault(a =>
                    a.Id == gameId && a.StatusId == (int) Enums.BTBStatusType.Started);

                if (foundGame == null)
                    return new ResultModel() {ErrorFlag = true, Message = "No record found"};

                foundGame.EndTime = DateTimeOffset.Now;
                foundGame.StatusId = (int) Enums.BTBStatusType.Completed;

                _dbContext.SaveChanges();

                var playFab = new PlayFab(_apiTrace, _dataContext, _logger, _tracker) { UserInfo = _userInfo, TitleId = "34997" };

                return await playFab.AddMoney(amountWon);
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"BeatTheBanker.cs EndGame({gameId}, {gameResult}, {amountWon}, {sourceId})");
                return new ResultModel() {ErrorFlag = true, Message = exception.Message};
            }
        }

        #endregion
    }
}
