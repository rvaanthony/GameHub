using System;
using GameHub.Models;
using GameHubAPI.Classes;
using GameHubAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace GameHubAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Properties

        private readonly User _user;
        private readonly ILog _log;

        public UserController(IDataContextProvider dataContextProvider, ITracker tracker, ILog log)
        {
            _user = new User(dataContextProvider, tracker, log);
            _log = log;
        }

        #endregion


        #region GET

        [HttpPost]
        [Route("login")]
        public void Login(UserModel userInfo)
        {
            var userContext = GetUserContextFromHeader();
            _user.Login(userInfo, userContext);
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