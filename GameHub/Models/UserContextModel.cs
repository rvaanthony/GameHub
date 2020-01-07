namespace GameHub.Models
{
    public class UserContextModel : BaseModel
    {
        public string UserId { get; set; }
        public string BrowserType { get; set; }
        public string BrowserVersion { get; set; }
        public string DeviceType { get; set; }
        public bool DeviceCrawler { get; set; }
        public string UserAgent { get; set; }
        public string HostIp { get; set; }
    }

}
