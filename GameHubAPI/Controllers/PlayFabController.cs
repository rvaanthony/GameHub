using GameHub.Models;
using GameHubAPI.Classes;
using GameHubAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using GameHub.Models.PlayFab;

namespace GameHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayFabController : ControllerBase
    {
        #region Properties

        private PlayFab _playFab;

        public PlayFabController(IApiHelperTrace apiTrace, IDataContextProvider dataContext, ILog logger, ITracker tracker)
        {
            _playFab = new PlayFab(apiTrace, dataContext, logger, tracker);
        }

        #endregion

        #region GET

        [HttpGet]
        [Route("{titleId}/leaderboard/{statName}")]
        public async Task<PlayFabUserLeaderBoard> GetLeaderBoard(string titleId, string statName)
        {
            GetUserContextFromHeader();
            _playFab.TitleId = titleId;
            var x = await _playFab.GetLeaderBoard(statName);
            return x;
        }

        #endregion

        #region POST

        [HttpPost]
        [Route("create/user/{titleId}")]
        public async Task<ResultModel> CreateNewUser(string titleId)
        {
            GetUserContextFromHeader();
            _playFab.TitleId = titleId;
            var x = await _playFab.CreateUser();
            return x;
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
                _playFab.UserInfo = userContext;
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