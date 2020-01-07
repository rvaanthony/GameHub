using System;
using GameHub.Models;
using Newtonsoft.Json;

namespace GameHubAPI.Models.PlayFab
{
    [JsonObject("SettingsForUser")]
    public class PlayFabLoginSettingsForUser
    {
        public bool NeedsAttribution { get; set; }
        public bool GatherDeviceInfo { get; set; }
        public bool GatherFocusInfo { get; set; }
    }
    [JsonObject("Entity")]
    public class PlayFabLoginEntity
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string TypeString { get; set; }
    }
    [JsonObject("EntityToken")]
    public class PlayFabLoginEntityToken
    {
        public string EntityToken { get; set; }
        public DateTime TokenExpiration { get; set; }
        public PlayFabLoginEntity Entity { get; set; }
    }

    [JsonObject("Data")]
    public class PlayFabLoginData
    {
        public string SessionTicket { get; set; }
        public string PlayFabId { get; set; }
        public bool NewlyCreated { get; set; }
        public PlayFabLoginSettingsForUser SettingsForUser { get; set; }
        public PlayFabLoginEntityToken EntityToken { get; set; }
    }

    public class PlayFabLoginResponse : BaseModel
    {
        public int code { get; set; }
        public string status { get; set; }
        public PlayFabLoginData data { get; set; }
    }
}
