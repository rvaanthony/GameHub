using System;
using System.Linq;
using GameHub.Models;
using GameHubAPI.Interfaces;
using GameHubAPI.Models;
using GameHubAPI.Models.DB;
using Newtonsoft.Json;
using UAParser;

namespace GameHubAPI.Classes
{
    public class ClientHelper
    {
        #region Properties

        private readonly GameHubContext _dbContext;
        private readonly ILog _logger;

        public ClientHelper(IDataContextProvider dataContextProvider, ILog logger)
        {
            _dbContext = dataContextProvider.GetGameHubContext();
            _logger = logger;
        }

        #endregion

        public ClientInfoModel GetClient(UserContextModel userContext)
        {
            try
            {
                var uaParser = Parser.GetDefault();
                var clientInfo = uaParser.Parse(userContext.UserAgent);

                var browser = CheckBrowser(userContext.BrowserType, userContext.BrowserVersion);
                if (browser.ErrorFlag)
                    return new ClientInfoModel() { ErrorFlag = true, Message = browser.Message };

                var device = CheckDevice(userContext.DeviceType);
                if(device.ErrorFlag)
                    return new ClientInfoModel() { ErrorFlag = true, Message = device.Message };

                var os = CheckOS(clientInfo.OS.Family, clientInfo.OS.Major);
                if (os.ErrorFlag)
                    return new ClientInfoModel() { ErrorFlag = true, Message = os.Message };

                var client = CheckClient(new ClientInfoModel()
                {
                    OSID = os.Id,
                    BrowserId = browser.Id,
                    DeviceId = device.Id,
                    IP = userContext.HostIp,
                    UserAgent = userContext.UserAgent,
                    Crawler = userContext.DeviceCrawler
                });

                return client;

            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"ClientHelper.cs GetClient({userContext})");
                return new ClientInfoModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        #region Private

        private BrowserModel CheckBrowser(string name, string version)
        {
            try
            {
                var foundRecord = _dbContext.TblBrowser.Where(a =>
                                      a.Active && a.Name == name.ToLower() && a.Version == version.ToLower()).Select(a => new BrowserModel()
                                  {
                                      Id = a.Id,
                                      Name = a.Name,
                                      Version = a.Version
                                  }).FirstOrDefault() ?? AddBrowserToDb(name, version);

                return foundRecord;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"ClientHelper.cs CheckBrowser({name}, {version})");
                return new BrowserModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private BrowserModel AddBrowserToDb(string name, string version)
        {
            try
            {
                var newRecord = new TblBrowser()
                {
                    Name = name.ToLower(),
                    Version = version.ToLower(),
                    Created = DateTimeOffset.Now,
                    Active = true
                };

                _dbContext.TblBrowser.Add(newRecord);
                _dbContext.SaveChanges();
                return new BrowserModel()
                {
                    Id = newRecord.Id,
                    Name = newRecord.Name,
                    Version = newRecord.Version
                };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"ClientHelper.cs AddBrowserToDb({name}, {version})");
                return new BrowserModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private OSModel CheckOS(string name, string version)
        {
            try
            {
                var foundRecord = _dbContext.TblOs.Where(a =>
                                      a.Active && a.Name == name.ToLower() && a.Version == version.ToLower()).Select(a => new OSModel()
                                  {
                                      Id = a.Id,
                                      Name = a.Name,
                                      Version = a.Version
                                  }).FirstOrDefault() ?? AddOSToDb(name, version);

                return foundRecord;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"ClientHelper.cs CheckOS({name}, {version})");
                return new OSModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private OSModel AddOSToDb(string name, string version)
        {
            try
            {
                var newRecord = new TblOs()
                {
                    Name = name.ToLower(),
                    Version = version.ToLower(),
                    Created = DateTimeOffset.Now,
                    Active = true
                };

                _dbContext.TblOs.Add(newRecord);
                _dbContext.SaveChanges();

                return new OSModel()
                {
                    Id = newRecord.Id,
                    Name = newRecord.Name,
                    Version = newRecord.Version
                };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"ClientHelper.cs AddOSToDb({name}, {version})");
                return new OSModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private DeviceModel CheckDevice(string name)
        {
            try
            {
                var foundRecord = _dbContext.TblDeviceType.Where(a => a.Active && a.Name == name.ToLower()).Select(a => new DeviceModel()
                {
                    Id = a.Id,
                    Name = a.Name
                }).FirstOrDefault() ?? AddDeviceToDb(name);

                return foundRecord;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"ClientHelper.cs CheckDevice({name})");
                return new DeviceModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private DeviceModel AddDeviceToDb(string name)
        {
            try
            {
                var newRecord = new TblDeviceType()
                {
                    Name = name.ToLower(),
                    Created = DateTimeOffset.Now,
                    Active = true
                };

                _dbContext.TblDeviceType.Add(newRecord);
                _dbContext.SaveChanges();
                return new DeviceModel()
                {
                    Id = newRecord.Id,
                    Name = newRecord.Name
                };
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"ClientHelper.cs AddDeviceToDb({name})");
                return new DeviceModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private ClientInfoModel CheckClient(ClientInfoModel clientInfo)
        {
            try
            {
                var foundRecord = _dbContext.TblClient.Where(a =>
                                      a.Active && a.Osid == clientInfo.OSID && a.BrowserId == clientInfo.BrowserId && a.DeviceTypeId == clientInfo.DeviceId && a.Ip == clientInfo.IP).Select(a => new ClientInfoModel()
                                  {
                                      Id = a.Id,
                                      BrowserId = a.BrowserId,
                                      DeviceId = a.DeviceTypeId,
                                      OSID = a.Osid,
                                      IP = a.Ip,
                                      UserAgent = a.UserAgent,
                                      Crawler = a.Crawler
                                  }).FirstOrDefault() ?? AddClientToDb(clientInfo);

                return foundRecord;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"ClientHelper.cs CheckClient({JsonConvert.SerializeObject(clientInfo)})");
                return new ClientInfoModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private ClientInfoModel AddClientToDb(ClientInfoModel clientInfo)
        {
            try
            {
                var newRecord = new TblClient()
                {
                    Osid = clientInfo.OSID,
                    BrowserId = clientInfo.BrowserId,
                    DeviceTypeId = clientInfo.DeviceId,
                    Ip = clientInfo.IP,
                    UserAgent = clientInfo.UserAgent,
                    Crawler = clientInfo.Crawler,
                    Created = DateTimeOffset.Now,
                    Active = true
                };

                _dbContext.TblClient.Add(newRecord);
                _dbContext.SaveChanges();

                clientInfo.Id = newRecord.Id;

                return clientInfo;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"ClientHelper.cs AddClientToDb({JsonConvert.SerializeObject(clientInfo)})");
                return new ClientInfoModel() { ErrorFlag = true, Message = exception.Message };
            }
        }
        #endregion
    }
}
