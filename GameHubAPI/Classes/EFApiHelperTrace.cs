using GameHub.Classes;
using GameHubAPI.Interfaces;
using GameHubAPI.Models.DB;
using System;
using System.Linq;

namespace GameHubAPI.Classes
{
    public class EFApiHelperTrace : IApiHelperTrace
    {
        private readonly GameHubContext _dbContext;

        public EFApiHelperTrace(IDataContextProvider dataContextProvider)
        {
            _dbContext = dataContextProvider.GetGameHubContext();
        }

        public IApiHelperTraceDetail OnStart(string url, string methodType, int? tokenId, string userId, string jsonData = null)
        {
            var methodId = (int) Enum.Parse(typeof(Enums.HttpMethodType), methodType);

            var newRecord = new TblApicallLog()
            {
                MethodTypeId = Convert.ToByte(methodId),
                Url = url,
                Created = DateTimeOffset.Now
            };

            if (jsonData != null)
                newRecord.Data = jsonData;
            if (tokenId != null && tokenId != 0)
                newRecord.TokenId = tokenId;

            if (userId != null)
            {
                var foundUser = _dbContext.TblUser.FirstOrDefault(a => a.Guid == userId);
                if(foundUser != null)
                    newRecord.UserId = foundUser.Id;
            }

            _dbContext.TblApicallLog.Add(newRecord);
            _dbContext.SaveChanges();

            return new EFApiHelperTraceDetail(newRecord, _dbContext);
        }
    }
}
