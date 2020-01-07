using System;

namespace GameHubAPI.Interfaces
{
    public interface IApiHelperTraceDetail
    {
        void OnComplete(string data, int statusCode);
        void OnException(Exception exception);
    }
}
