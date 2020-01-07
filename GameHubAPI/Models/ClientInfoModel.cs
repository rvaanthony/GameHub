using GameHub.Models;

namespace GameHubAPI.Models
{
    public class ClientInfoModel : BaseModel
    {
        public int Id { get; set; }
        public BrowserModel Browser { get; set; }
        public int BrowserId { get; set; }
        public DeviceModel Device { get; set; }
        public int DeviceId { get; set; }
        public OSModel OS { get; set; }
        public int OSID { get; set; }
        public string IP { get; set; }
        public string UserAgent { get; set; }
        public bool Crawler { get; set; }
    }
}
