using System;
using System.Collections.Generic;
using System.Text;

namespace GameHub.Models.PlayFab
{
    public class PlayFabUserStat : BaseModel
    {
        public string StatisticName { get; set; }
        public int Version { get; set; }
        public int Value { get; set; }
    }

    public class PlayFabUserStats : BaseModel
    {
        public List<PlayFabUserStat> Statistics { get; set; }
    }
}
