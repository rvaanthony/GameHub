using GameHub.Classes;
using GameHub.Models;
using GameHub.Models.BTB;
using GameHubAPI.Classes;
using GameHubAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BTBController : ControllerBase
    {
        #region Properties
        
        private BeatTheBanker _BTB;

        public BTBController(IApiHelperTrace apiTrace, IDataContextProvider dataContext, ILog logger, ITracker tracker)
        {
            _BTB = new BeatTheBanker(apiTrace, dataContext, logger, tracker);
        }

        #endregion

        #region GET

        [HttpGet]
        [Route("game/board")]
        public BTBCaseAmountListModel GetGameBoard()
        {
            GetUserContextFromHeader();
            return _BTB.GetGameBoard();
        }

        #endregion

        #region POST

        [HttpPost]
        [Route("game/info")]
        public BTBGameModel GetGameInfo([FromBody]BTBGameModel gameInfo)
        {
            GetUserContextFromHeader();
            return _BTB.GetGameInfo(gameInfo.GameGuid);
        }

        [HttpPost]
        [Route("New/Game")]
        public BTBGameModel NewGame()
        {
            GetUserContextFromHeader();
            var gameInfo = _BTB.InitializeNewGame();
            return gameInfo;
        }

        [HttpPost]
        [Route("open/case")]
        public BTBBankerOffer OpenCase([FromBody]BTBGameCaseModel gameCase)
        {
            GetUserContextFromHeader();
            var results = _BTB.OpenGameCase(gameCase);
            return results;
        }

        [HttpPost]
        [Route("accept/offer")]
        public async Task<ResultModel> AcceptOffer([FromBody]BTBBankerOffer offer)
        {
            GetUserContextFromHeader();
            var results = await _BTB.UpdateBankOffer(offer, Enums.BTBStatusType.Accepted);
            return results;
        }

        [HttpPost]
        [Route("decline/offer")]
        public async Task<ResultModel> DeclineOffer([FromBody]BTBBankerOffer offer)
        {
            GetUserContextFromHeader();
            var results = await _BTB.UpdateBankOffer(offer, Enums.BTBStatusType.Declined);
            return results;
        }

        [HttpPost]
        [Route("swap/final/case")]
        public BTBGameCaseModel SwapFinalCase([FromBody]BTBGameModel gameInfo)
        {
            GetUserContextFromHeader();
            var results = _BTB.SwapFinalCase(gameInfo.GameGuid);
            return results;
        }

        [HttpPost]
        [Route("case/data/{caseGuid}")]
        public BTBGameCaseModel GetCaseData(string caseGuid)
        {
            GetUserContextFromHeader();
            var caseData = _BTB.CaseData(caseGuid);
            return caseData;
        }

        #endregion

        #region Private Functions

        private UserContextModel GetUserContextFromHeader()
        {
            try
            {
                Request.Headers.TryGetValue("UserContext", out var headerValues);
                var userAgent = headerValues.FirstOrDefault();
                var userContext = JsonConvert.DeserializeObject<UserContextModel>(userAgent);
                _BTB._userInfo = userContext;
                return userContext;
            }
            catch (Exception exception)
            {
                return new UserContextModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        #endregion
    }
}