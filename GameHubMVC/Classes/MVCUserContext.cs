using GameHubMVC.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Wangkanai.Detection;

namespace GameHubMVC.Classes
{
    public class MVCUserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDetection _detection;

        public MVCUserContext(IHttpContextAccessor httpContextAccessor, IDetection detection)
        {
            _httpContextAccessor = httpContextAccessor;
            _detection = detection;
        }

        public string UserId => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        public string BrowserType => _detection.Browser.Type.ToString();
        public string BrowserVersion => _detection.Browser.Version == null ? null : _detection.Browser.Version.ToString();
        public string DeviceType => _detection.Device.Type.ToString();
        public bool DeviceCrawler => _detection.Device.Crawler;
        public string UserAgent => _detection.UserAgent?.ToString();
        public string HostIp => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString() ?? "";
        public HttpContext HttpContext => _httpContextAccessor.HttpContext;
    }

}
