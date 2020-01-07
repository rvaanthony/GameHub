using System;
using GameHub.Classes;

namespace GameHubAPI.Interfaces
{
    public interface ILog
    {
        string CorrelationId { get; set; }
        void LogInfo(string message, string info, string userId = null);
        void LogError(Exception exception, string info, Enums.SeverityCode severityCode = Enums.SeverityCode.Low, int? userId = null);
    }
}
