using System.Net.Http;
using System.Threading.Tasks;
using GameHub.Models;
using GameHub.Models.BTB;
using GameHubMVC.Classes;
using GameHubMVC.Interfaces;
using GameHubMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GameHubMVC.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BTBController : ControllerBase
    {
        #region Properties
        private readonly IUserContext _userContext;
        private readonly IOptions<AzureAdB2COptions> _azureAdB2COptions;

        public BTBController(IUserContext userContext, IOptions<AzureAdB2COptions> azureAdB2COptions)
        {
            _userContext = userContext;
            _azureAdB2COptions = azureAdB2COptions;

        }
        #endregion

        #region GET

        [HttpGet]
        [Route("game/board")]
        public async Task<BTBCaseAmountListModel> GetGameBoard()
        {
            var x = await ApiHelper.ApiCallAsync($"{_azureAdB2COptions.Value.ApiUrl}/btb/game/board", null, HttpMethod.Get, _userContext, _azureAdB2COptions);
            return JsonConvert.DeserializeObject<BTBCaseAmountListModel>(x);
        }


        #endregion

        #region POST

        [HttpPost]
        [Route("game/info")]
        public async Task<BTBGameModel> GetGameInfo([FromBody]string gameId)
        {
            var json = JsonConvert.SerializeObject(new BTBGameModel() { GameGuid = gameId });
            var x = await ApiHelper.ApiCallAsync($"{_azureAdB2COptions.Value.ApiUrl}/btb/game/info", json, HttpMethod.Post, _userContext, _azureAdB2COptions);
            return JsonConvert.DeserializeObject<BTBGameModel>(x);
        }

        [HttpPost]
        [Route("new/game")]
        public async Task<BTBGameModel> NewGame()
        {
            var x = await ApiHelper.ApiCallAsync($"{_azureAdB2COptions.Value.ApiUrl}/btb/new/game", null, HttpMethod.Post, _userContext, _azureAdB2COptions);
            return JsonConvert.DeserializeObject<BTBGameModel>(x);

        }

        [HttpPost]
        [Route("open/case")]
        public async Task<BTBBankerOffer> OpenCaseAsync([FromBody]BTBGameCaseModel gameCase)
        {
            var json = JsonConvert.SerializeObject(gameCase);
            var x = await ApiHelper.ApiCallAsync($"{_azureAdB2COptions.Value.ApiUrl}/btb/open/case", json, HttpMethod.Post, _userContext, _azureAdB2COptions);
            return JsonConvert.DeserializeObject<BTBBankerOffer>(x);
        }

        [HttpPost]
        [Route("accept/offer")]
        public async Task<ResultModel> AcceptOffer([FromBody]BTBBankerOffer offer)
        {
            var json = JsonConvert.SerializeObject(offer);
            var x = await ApiHelper.ApiCallAsync($"{_azureAdB2COptions.Value.ApiUrl}/btb/accept/offer", json, HttpMethod.Post, _userContext, _azureAdB2COptions);
            return JsonConvert.DeserializeObject<ResultModel>(x);
        }

        [HttpPost]
        [Route("decline/offer")]
        public async Task<ResultModel> DeclineOffer([FromBody]BTBBankerOffer offer)
        {
            var json = JsonConvert.SerializeObject(offer);
            var x = await ApiHelper.ApiCallAsync($"{_azureAdB2COptions.Value.ApiUrl}/btb/decline/offer", json, HttpMethod.Post, _userContext, _azureAdB2COptions);
            return JsonConvert.DeserializeObject<ResultModel>(x);
        }

        [HttpPost]
        [Route("swap/final/case")]
        public async Task<BTBGameCaseModel> SwapFinalCase([FromBody]BTBGameModel gameInfo)
        {
            var json = JsonConvert.SerializeObject(gameInfo);
            var x = await ApiHelper.ApiCallAsync($"{_azureAdB2COptions.Value.ApiUrl}/btb/swap/final/case", json, HttpMethod.Post, _userContext, _azureAdB2COptions);
            return JsonConvert.DeserializeObject<BTBGameCaseModel>(x);
        }

        [HttpPost]
        [Route("case/data/{caseGuid}")]
        public async Task<BTBGameCaseModel> GetCaseData(string caseGuid)
        {
            var x = await ApiHelper.ApiCallAsync($"{_azureAdB2COptions.Value.ApiUrl}/btb/case/data/{caseGuid}", null, HttpMethod.Post, _userContext, _azureAdB2COptions);
            return JsonConvert.DeserializeObject<BTBGameCaseModel>(x);
        }

        #endregion
    }
}