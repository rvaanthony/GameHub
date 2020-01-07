namespace GameHubAPI.Utils
{
    public static class PlayFabGlobals
    {
        public static string ApiUri = "https://{0}.playfabapi.com/{1}";

        #region Game Titles

        public static string BeatTheBanker = "";

        #endregion

        #region Api EndPoints

        public static string LoginInWithCustomId = "Client/LoginWithCustomID";
        public static string AddUsernamePassword = "Client/AddUsernamePassword";
        public static string AddUserVirtualCurrency = "Client/AddUserVirtualCurrency";
        public static string UpdatePlayerStatistics = "Client/UpdatePlayerStatistics";
        public static string GetLeaderboard = "Client/GetLeaderboard";

        #endregion

        public static string GameCurrency = "AB";

    }
}
