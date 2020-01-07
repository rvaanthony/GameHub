using GameHub.Models;
using GameHub.Models.PlayFab;
using GameHubAPI.Interfaces;
using GameHubAPI.Models.DB;
using GameHubAPI.Models.PlayFab;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GameHub.Classes;
using static System.String;

namespace GameHubAPI.Classes
{
    public class PlayFab
    {
        #region Properties

        public UserContextModel UserInfo { get; set; }
        public string TitleId { get; set; }

        private readonly IApiHelperTrace _apiTrace;
        private readonly GameHubContext _dbContext;
        private readonly ITracker _tracker;
        private readonly ILog _logger;

        public PlayFab(IApiHelperTrace apiTrace, IDataContextProvider dataContext, ILog logger, ITracker tracker)
        {
            _dbContext = dataContext.GetGameHubContext();
            _apiTrace = apiTrace;
            _tracker = tracker;
            _logger = logger;
        }

        #endregion

        public async Task<ResultModel> CreateUser()
        {
            try
            {
                if (UserInfo == null)
                    return new ResultModel() { ErrorFlag = true, Message = "Users not defined.." };

                if(GetPlayFabUser() != null)
                    return new ResultModel() {Success = true};

                var playFabResults = await LogUserInPlayFab(true);
                if(playFabResults.ErrorFlag)
                    return new ResultModel() { ErrorFlag = true, Message = playFabResults.Message };

                var results = InsertPlayFabUserData(playFabResults);
                if (results.ErrorFlag)
                    return results;

                _tracker.TrackAction(Enums.TrackingActionType.PlayFabUserCreated, Enums.TableEntity.tblPlayFabUser, results.Data, UserInfo.UserId, JsonConvert.SerializeObject(playFabResults));

                return results;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs CreateUser()");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        public async Task<ResultModel> AddMoney(int amount)
        {
            try
            {
                var playFabUserInfo = GetPlayFabUser();
                if (playFabUserInfo == null)
                    return new ResultModel() { ErrorFlag = true, Message = "No PlayFab user found" };

                var token = await GetActiveToken();
                if (token.ErrorFlag)
                    return new ResultModel() { ErrorFlag = true, Message = token.Message };

                var results = await UpdateCurrency(new PlayFabVirtualCurrencyModel()
                {
                    Amount = amount,
                    PlayFabId = playFabUserInfo.PlayFabId,
                    VirtualCurrency = Utils.PlayFabGlobals.GameCurrency
                }, token);

                if(results.ErrorFlag)
                    return new ResultModel();

                return await UpdateUserStats("MoneyWon", amount, token);
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs AddMoney({amount})");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        public async Task<PlayFabLoginResponse> LogUserIn()
        {
            try
            {
                var results = await LogUserInPlayFab();
                if (results.ErrorFlag)
                    return results;

                if (SetTokensToInactive().ErrorFlag)
                    return results;

                results = AddTokenToDb(results);

                return results;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs LogUserIn()");
                return new PlayFabLoginResponse() {ErrorFlag = true, Message = exception.Message};
            }
        }

        public async Task<PlayFabUserLeaderBoard> GetLeaderBoard(string statName)
        {
            try
            {
                var token = await GetActiveToken();
                if (token.ErrorFlag)
                    return new PlayFabUserLeaderBoard() { ErrorFlag = true, Message = token.Message };

                var apiUrl = Format(Utils.PlayFabGlobals.ApiUri, TitleId,
                    Utils.PlayFabGlobals.GetLeaderboard);

                var data = new PlayFabUserLeaderBoard()
                {
                    StartPosition = 0,
                    MaxResultsCount = 20,
                    StatisticName = statName
                };

                var json = JsonConvert.SerializeObject(data);
                var res = await ApiHelper.ApiCallAsync(apiUrl, json, HttpMethod.Post, _apiTrace, null, null, null, token.data.SessionTicket);
                if(res.ErrorFlag)
                    return new PlayFabUserLeaderBoard() { ErrorFlag = true, Message = res.Message };

                var x = JsonConvert.DeserializeObject<PlayFabUserLeaderBoard>(res.Data);

                _tracker.TrackAction(Enums.TrackingActionType.PlayFabGetLeaderboard, null, null, UserInfo.UserId, JsonConvert.SerializeObject(data));

                return x;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs GetLeaderBoard({statName})");
                return new PlayFabUserLeaderBoard(){ ErrorFlag = true, Message = exception.Message };
            }
        }

        #region Analytics

        public void ReportDeviceInfo()
        {
            try
            {

            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs ReportDeviceInfo()");
            }
        }

        #endregion

        #region Private Functions

        private async Task<PlayFabLoginResponse> LogUserInPlayFab(bool createAccount = false)
        {
            try
            {
                var apiUrl = Format(Utils.PlayFabGlobals.ApiUri, TitleId,
                    Utils.PlayFabGlobals.LoginInWithCustomId);

                var data = new LoginWithCustomIDModel()
                {
                    CreateAccount = createAccount,
                    CustomId = UserInfo.UserId,
                    TitleId = TitleId
                };

                var json = JsonConvert.SerializeObject(data);

                var res = await ApiHelper.ApiCallAsync(apiUrl, json, HttpMethod.Post, _apiTrace, UserInfo.UserId);
                if(res.ErrorFlag)
                    return new PlayFabLoginResponse() { ErrorFlag = true, Message = res.Message };

                _tracker.TrackAction(Enums.TrackingActionType.PlayFabLogin, null, null, UserInfo.UserId, JsonConvert.SerializeObject(data));

                return JsonConvert.DeserializeObject<PlayFabLoginResponse>(res.Data);
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs LogUserInPlayFab({createAccount})");
                return new PlayFabLoginResponse() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private ResultModel InsertPlayFabUserData(PlayFabLoginResponse playFabUserInfo)
        {
            try
            {
                var newRecord = new TblPlayFabUser()
                {
                    Active = true,
                    Created = DateTimeOffset.Now,
                    PlayFabId = playFabUserInfo.data.PlayFabId,
                    UserId = _dbContext.TblUser.Where(a => a.Guid == UserInfo.UserId).Select(a => a.Id).FirstOrDefault()
                };

                _dbContext.TblPlayFabUser.Add(newRecord);
                _dbContext.SaveChanges();

                return new ResultModel() { Success = true, Data = newRecord.Id.ToString() };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs InsertPlayFabUserData({JsonConvert.SerializeObject(playFabUserInfo)})");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private PlayFabUserModel GetPlayFabUser()
        {
            try
            {
                var foundRecord = _dbContext.TblPlayFabUser.Where(a => a.Active && a.User.Guid == UserInfo.UserId).Select(a => new PlayFabUserModel()
                {
                    Id = a.Id,
                    PlayFabId = a.PlayFabId
                }).FirstOrDefault();
                return  foundRecord;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs GetPlayFabUser()");
                return new PlayFabUserModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private async Task<PlayFabVirtualCurrencyModel> UpdateCurrency(PlayFabVirtualCurrencyModel currency, PlayFabLoginResponse tokenInfo)
        {
            try
            {
                var apiUrl = Format(Utils.PlayFabGlobals.ApiUri, TitleId,
                    Utils.PlayFabGlobals.AddUserVirtualCurrency);

                var json = JsonConvert.SerializeObject(currency);

                var res = await ApiHelper.ApiCallAsync(apiUrl, json, HttpMethod.Post, _apiTrace, UserInfo.UserId, null, null, tokenInfo.data.SessionTicket);
                if(res.ErrorFlag)
                    return new PlayFabVirtualCurrencyModel() { ErrorFlag = true, Message = res.Message };

                var d = JsonConvert.DeserializeObject<PlayFabVirtualCurrencyModel>(res.Data);

                return d;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs UpdateCurrency({JsonConvert.SerializeObject(currency)}, {JsonConvert.SerializeObject(tokenInfo)})");
                return new PlayFabVirtualCurrencyModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private async Task AddUsernameToAccount(string userName, string email, string password)
        {
            try
            {
                var apiUrl = Format(Utils.PlayFabGlobals.ApiUri, TitleId,
                    Utils.PlayFabGlobals.AddUsernamePassword);

                var data = new AddUsernamePassword()
                {
                    Username = userName,
                    Email = email,
                    Password = password
                };

                var json = JsonConvert.SerializeObject(data);
                var res = await ApiHelper.ApiCallAsync(apiUrl, json, HttpMethod.Post, _apiTrace, null, null, null, "");
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs AddUsernameToAccount()");
            }
        }

        private PlayFabLoginResponse AddTokenToDb(PlayFabLoginResponse data)
        {
            try
            {
                var newRecord = new TblPlayFabToken()
                {
                    Active = true, 
                    PlayFabUserId = _dbContext.TblPlayFabUser.Where(a => a.Active && a.PlayFabId == data.data.PlayFabId).Select(a => a.Id).FirstOrDefault(),
                    SessionTicket = data.data.SessionTicket,
                    EntityToken = data.data.EntityToken.EntityToken,
                    TokenExperation = data.data.EntityToken.TokenExpiration,
                    Created = DateTimeOffset.Now
                };

                _dbContext.TblPlayFabToken.Add(newRecord);
                _dbContext.SaveChanges();

                return data;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs AddTokenToDb({JsonConvert.SerializeObject(data)})");
                return new PlayFabLoginResponse() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private ResultModel SetTokensToInactive()
        {
            try
            {
                var foundRecords = _dbContext.TblPlayFabToken.Where(a => a.PlayFabUser.User.Guid == UserInfo.UserId && a.Active).ToList();

                foreach (var record in foundRecords)
                {
                    record.Active = false;
                    record.Modified = DateTimeOffset.Now;
                }

                _dbContext.SaveChanges();

                return new ResultModel() { Success = true };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs SetTokensToInactive()");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private async Task<PlayFabLoginResponse> GetActiveToken()
        {
            try
            {
                var foundRecord = _dbContext.TblPlayFabToken
                    .Where(a => a.Active && a.PlayFabUser.User.Guid == UserInfo.UserId).Select(a => new PlayFabLoginResponse()
                    {
                        data = new PlayFabLoginData()
                        {
                            PlayFabId = a.PlayFabUser.PlayFabId,
                            SessionTicket = a.SessionTicket,
                            EntityToken = new PlayFabLoginEntityToken()
                            {
                                EntityToken = a.EntityToken,
                                TokenExpiration = DateTime.Parse(a.TokenExperation.ToString())
                            }
                        }
                    }).FirstOrDefault();

                if (foundRecord == null || foundRecord.data.EntityToken.TokenExpiration < DateTimeOffset.Now)
                    return await LogUserIn();

                return foundRecord;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs GetActiveToken()");
                return new PlayFabLoginResponse() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private async Task<ResultModel> UpdateUserStats(string statName, int value, PlayFabLoginResponse tokenInfo)
        {
            try
            {
                var apiUrl = Format(Utils.PlayFabGlobals.ApiUri, TitleId,
                    Utils.PlayFabGlobals.UpdatePlayerStatistics);

                var data = new PlayFabUserStats(){ Statistics = new List<PlayFabUserStat>() };

                data.Statistics.Add(new PlayFabUserStat()
                {
                    StatisticName = statName,
                    Value = value
                });

                var json = JsonConvert.SerializeObject(data);
                var res = await ApiHelper.ApiCallAsync(apiUrl, json, HttpMethod.Post, _apiTrace, null, null, null, tokenInfo.data.SessionTicket);

                _tracker.TrackAction(Enums.TrackingActionType.PlayFabUpdateUserStats, null, null, UserInfo.UserId, JsonConvert.SerializeObject(data));


                return res.ErrorFlag ? new ResultModel() {ErrorFlag = true, Message = res.Message} : new ResultModel() { Success = res.Code == HttpStatusCode.OK ? true: false};
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"PlayFab.cs UpdateUserStats({statName}, {value}, {JsonConvert.SerializeObject(tokenInfo)})");
                return new ResultModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        #endregion
    }
}
