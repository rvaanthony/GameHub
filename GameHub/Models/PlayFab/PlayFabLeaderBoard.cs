using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameHub.Models.PlayFab
{
    public class PlayFabUserLeaderBoard : BaseModel
    {
        public string StatisticName { get; set; }
        public int StartPosition { get; set; }
        public int MaxResultsCount { get; set; }
        public int code { get; set; }
        public string status { get; set; }
        public PlayFabLeaderBoardData data { get; set; }
    }

    [JsonObject("Leaderboard")]
    public class PlayFabLeaderboard
    {
        public string PlayFabId { get; set; }
        public string DisplayName { get; set; }
        public int StatValue { get; set; }
        public int Position { get; set; }
    }
    [JsonObject("Data")]
    public class PlayFabLeaderBoardData
    {
        public List<PlayFabLeaderboard> Leaderboard { get; set; }
        public int Version { get; set; }
    }
}
