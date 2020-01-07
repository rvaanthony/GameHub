using System.Net;

namespace GameHub.Models
{
    public class ApiModel : BaseModel
    {
        public string Data { get; set; }
        public HttpStatusCode Code { get; set; }
    }
}
