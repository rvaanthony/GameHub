using GameHub.Models;
using GameHubMVC.Classes;
using GameHubMVC.Interfaces;
using GameHubMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using GameHub.Models.PlayFab;

namespace GameHubMVC.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayFabController : ControllerBase
    {
        #region Properties
        private readonly IUserContext _userContext;
        private readonly IOptions<AzureAdB2COptions> _azureAdB2COptions;

        public PlayFabController(IUserContext userContext, IOptions<AzureAdB2COptions> azureAdB2COptions)
        {
            _userContext = userContext;
            _azureAdB2COptions = azureAdB2COptions;

        }
        #endregion

        [HttpGet]
        [Route("create/user/{titleId}")]
        public async Task<ResultModel> CreateNewUser(string titleId)
        {
            var x = await ApiHelper.ApiCallAsync($"{_azureAdB2COptions.Value.ApiUrl}/playfab/create/user/{titleId}", null, HttpMethod.Post, _userContext, _azureAdB2COptions);
            return JsonConvert.DeserializeObject<ResultModel>(x);
        }

        [HttpGet]
        [Route("{titleId}/leaderboard/{statName}")]
        public async Task<PlayFabUserLeaderBoard> GetLeaderBoard(string titleId, string statName)
        {
            var x = await ApiHelper.ApiCallAsync($"{_azureAdB2COptions.Value.ApiUrl}/playfab/{titleId}/leaderboard/{statName}", null, HttpMethod.Get, _userContext, _azureAdB2COptions);
            return JsonConvert.DeserializeObject<PlayFabUserLeaderBoard>(x);
        }
    }
}