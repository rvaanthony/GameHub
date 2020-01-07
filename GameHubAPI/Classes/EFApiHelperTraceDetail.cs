using GameHubAPI.Interfaces;
using System;
using GameHubAPI.Models.DB;

namespace GameHubAPI.Classes
{
    public class EFApiHelperTraceDetail : IApiHelperTraceDetail
    {
        private readonly TblApicallLog _logRecord;
        private readonly GameHubContext _dbContext;

        public EFApiHelperTraceDetail(TblApicallLog logRecord, GameHubContext dbContext)
        {
            _logRecord = logRecord;
            _dbContext = dbContext;
        }

        public void OnComplete(string data, int statusCode)
        {
            _logRecord.Response = data;
            _logRecord.ResponseStatusCodeId = statusCode;
            _logRecord.Modified = DateTimeOffset.Now;
            
            _dbContext.SaveChanges();
        }

        public void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
