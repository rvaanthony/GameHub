using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GameHub.Models;
using GameHubAPI.Interfaces;
using static System.String;

namespace GameHubAPI.Classes
{
    public static class ApiHelper
    {
        public static async Task<ApiModel> ApiCallAsync(string url, string jsonData, HttpMethod methodType, IApiHelperTrace trace, string userId = null, FormUrlEncodedContent content = null, string token = null, string xAuth = null)
        {
            IApiHelperTraceDetail traceDetail = null;

            try
            {
                traceDetail = trace?.OnStart(url, methodType.ToString(), null, userId, jsonData);
            }
            catch (Exception)
            {
                //Do nothing, just want it to bomb
            }

            try
            {
                var client = new HttpClient();
                using (var http = new HttpClient())
                {
                    var request = new HttpRequestMessage(methodType, url);
                    if (jsonData != null)
                        request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    if (content != null)
                        request.Content = content;
                    if (token != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    else if(xAuth != null)
                        request.Headers.Add("X-Authentication", xAuth);

                    var httpResponse = await client.SendAsync(request);
                    if (httpResponse.Content == null) return new ApiModel() {ErrorFlag = true, Message = "No Content"};
                    var responseString = "";
                    var statusCode = (int) httpResponse.StatusCode;
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

                    traceDetail?.OnComplete(responseString, statusCode);

                    return new ApiModel() { Data = responseString, Code = httpResponse.StatusCode };
                }
            }
            catch (Exception ex)
            {
                traceDetail?.OnException(ex);
                return new ApiModel() { ErrorFlag = true, Message = ex.Message };
            }
        }

    }
}
