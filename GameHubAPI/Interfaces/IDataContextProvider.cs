using GameHubAPI.Models.DB;
using Microsoft.Extensions.Configuration;

namespace GameHubAPI.Interfaces
{
    public interface IDataContextProvider
    {
        GameHubContext GetGameHubContext();
        IConfiguration GetConfiguration();
    }
}
