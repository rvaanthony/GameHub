using GameHub.Models;
using GameHubMVC.Interfaces;
using GameHubMVC.Models;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace GameHubMVC.Classes
{
    public static class ApiHelper
    {
        public static async Task<string> ApiCallAsync(string url, string jsonData, HttpMethod methodType, IUserContext userContext, IOptions<AzureAdB2COptions> azureAdB2COptions, string signedInUserId = null)
        {
            try
            {
                var client = new HttpClient();
                var azureB2COptions = azureAdB2COptions.Value;
                using (var http = new HttpClient())
                {
                    // Retrieve the token with the specified scopes
                    var scope = azureB2COptions.ApiScopes.Split(' ');

                    if(signedInUserId == null)
                        signedInUserId = userContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                    var cca =
                    ConfidentialClientApplicationBuilder.Create(azureB2COptions.ClientId)
                        .WithRedirectUri(azureB2COptions.RedirectUri)
                        .WithClientSecret(azureB2COptions.ClientSecret)
                        .WithB2CAuthority(azureB2COptions.Authority)
                        .Build();
                    new MSALStaticCache(signedInUserId, userContext.HttpContext).EnablePersistence(cca.UserTokenCache);

                    var accounts = await cca.GetAccountsAsync();
                    var result = await cca.AcquireTokenSilent(scope, accounts.FirstOrDefault())
                        .ExecuteAsync();

                    var request = new HttpRequestMessage(methodType, url);

                    // Add token to the Authorization header and make the request
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

                    if (jsonData != null)
                        request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    request.Headers.Add("UserContext", JsonConvert.SerializeObject(new UserContextModel() {
                        UserAgent = userContext.UserAgent,
                        BrowserType = userContext.BrowserType,
                        BrowserVersion = userContext.BrowserVersion,
                        DeviceCrawler = userContext.DeviceCrawler,
                        DeviceType = userContext.DeviceType,
                        HostIp = userContext.HostIp,
                        UserId = userContext.UserId == "" ? signedInUserId : userContext.UserId
                    }));

                    var httpResponse = await client.SendAsync(request);
                    if (httpResponse.Content == null) return Empty;

                    var responseString = "";
                    switch (httpResponse.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            responseString = await httpResponse.Content.ReadAsStringAsync();
                            break;
                        case HttpStatusCode.Unauthorized:
                            responseString = $"Please sign in again. {httpResponse.ReasonPhrase}";
                            break;
                        default:
                            var tellMeMore = await httpResponse.Content.ReadAsStringAsync();
                            responseString = $"Error calling API. StatusCode=${httpResponse.StatusCode}";
                            break;
                    }
                    return responseString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
