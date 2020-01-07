using Microsoft.AspNetCore.Http;

namespace GameHubMVC.Interfaces
{
    public interface IUserContext
    {
        string UserId { get; }
        string BrowserType { get; }
        string BrowserVersion { get; }
        string DeviceType { get; }
        bool DeviceCrawler { get; }
        string UserAgent { get; }
        string HostIp { get; }
        HttpContext HttpContext { get; }
    }

}
