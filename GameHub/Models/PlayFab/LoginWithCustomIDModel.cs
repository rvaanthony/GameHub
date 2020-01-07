namespace GameHubAPI.Models.PlayFab
{
    public class LoginWithCustomIDModel
    {
        public string CustomId { get; set; }
        public bool CreateAccount { get; set; }
        public string TitleId { get; set; }
    }
}
