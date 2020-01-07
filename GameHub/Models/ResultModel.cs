namespace GameHub.Models
{
    public class ResultModel : BaseModel
    {
        public bool Success { get; set; }
        public bool Found { get; set; }
        public string Data { get; set; }
    }

}
