using GameHubAPI.Interfaces;
using GameHubAPI.Models.DB;
using Microsoft.Extensions.Configuration;

namespace GameHubAPI.Classes
{
    public class DataContextProvider : IDataContextProvider
    {
        #region Properties

        private readonly GameHubContext _dbContext;
        private readonly IConfiguration _configuration;

        public DataContextProvider(GameHubContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        #endregion
        public GameHubContext GetGameHubContext()
        {
            return _dbContext;
        }

        public IConfiguration GetConfiguration()
        {
            return _configuration;
        }
    }
}
