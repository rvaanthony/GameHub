namespace GameHubAPI.Interfaces
{
    public interface IApiHelperTrace
    {
        IApiHelperTraceDetail OnStart(string url, string methodType, int? tokenId, string userId, string jsonData = null);
    }
}
