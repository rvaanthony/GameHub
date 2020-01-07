using GameHub.Models;

namespace GameHubAPI.Models
{
    public class OSModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
