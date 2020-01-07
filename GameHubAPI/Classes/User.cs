using GameHub.Classes;
using GameHub.Models;
using GameHubAPI.Interfaces;
using GameHubAPI.Models.DB;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace GameHubAPI.Classes
{
    public class User
    {
        #region Properties

        private readonly GameHubContext _dbContext;
        private readonly IDataContextProvider _dataContextProvider;
        private readonly ITracker _tracker;
        private readonly ILog _log;

        public User(IDataContextProvider dataContextProvider, ITracker tracker, ILog log)
        {
            _dbContext = dataContextProvider.GetGameHubContext();
            _dataContextProvider = dataContextProvider;
            _tracker = tracker;
            _log = log;
        }

        #endregion

        #region Public Functions

        public void Login(UserModel userInfo, UserContextModel userContext)
        {
            try
            {
                _log.LogInfo("Start Login", $"UserInfo: {JsonConvert.SerializeObject(userInfo)} /n UserContext: {JsonConvert.SerializeObject(userContext)}", userInfo.GUID);

                userInfo = VerifyUserInDb(userInfo);
                if(userInfo.ErrorFlag)
                    return;

                LogUserLogin(userInfo, userContext);

                _log.LogInfo("End Login", $"UserInfo: {JsonConvert.SerializeObject(userInfo)} /n UserContext: {JsonConvert.SerializeObject(userContext)}", userInfo.GUID);
            }
            catch (Exception exception)
            {
                _log.LogError(exception, $"User.cs Login({JsonConvert.SerializeObject(userInfo)}, {JsonConvert.SerializeObject(userContext)})", Enums.SeverityCode.Medium);
                return;
            }
        }

        #endregion

        #region Private Functions

        private UserModel VerifyUserInDb(UserModel userInfo)
        {
            try
            {
                var inDb = CheckForUserInDb(userInfo.GUID);
                if (inDb == null)
                {
                    userInfo = AddUserToDb(userInfo);
                }
                else if (inDb.ErrorFlag)
                {
                    userInfo.Message = inDb.Message;
                    userInfo.ErrorFlag = true;
                }
                else
                    userInfo = CheckForUserInfoChange(userInfo, inDb);
            }
            catch (Exception exception)
            {
                _log.LogError(exception, $"User.cs VerifyUserInDb({JsonConvert.SerializeObject(userInfo)})", Enums.SeverityCode.High);
                userInfo.ErrorFlag = true;
                userInfo.Message = exception.Message;
            }

            return userInfo;
        }

        private UserModel CheckForUserInfoChange(UserModel userInfo, UserModel dbUserInfo)
        {
            try
            {
                var noChange = !(userInfo.Email != dbUserInfo.Email || userInfo.FirstName != dbUserInfo.FirstName || userInfo.LastName != dbUserInfo.LastName || userInfo.Username != dbUserInfo.Username);

                if (!noChange)
                    return UpdateUserDbInfo(userInfo);
            }
            catch (Exception exception)
            {
                _log.LogError(exception, $"User.cs CheckForUserInfoChange({JsonConvert.SerializeObject(userInfo)}, {JsonConvert.SerializeObject(dbUserInfo)})", Enums.SeverityCode.High);
                dbUserInfo.ErrorFlag = true;
                dbUserInfo.Message = exception.Message;
            }

            return dbUserInfo;
        }

        private UserModel UpdateUserDbInfo(UserModel userInfo)
        {
            try
            {
                var foundRecord = _dbContext.TblUser.FirstOrDefault(a => a.Guid == userInfo.GUID);
                if (foundRecord == null)
                {
                    userInfo.ErrorFlag = true;
                    userInfo.Message = "No user found...";
                    return userInfo;
                }

                foundRecord.Email = userInfo.Email;
                foundRecord.Username = userInfo.Username;
                foundRecord.FirstName = userInfo.FirstName;
                foundRecord.LastName = userInfo.LastName;
                foundRecord.Modified = DateTimeOffset.Now;

                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                _log.LogError(exception, $"User.cs UpdateUserDbInfo({JsonConvert.SerializeObject(userInfo)})", Enums.SeverityCode.High);
                userInfo.ErrorFlag = true;
                userInfo.Message = exception.Message;
            }

            return userInfo;
        }

        private UserModel CheckForUserInDb(string guid)
        {
            try
            {
                var foundRecord = _dbContext.TblUser.Where(a => a.Guid == guid).Select(a => new UserModel()
                    {
                        Id = a.Id,
                        GUID = a.Guid,
                        Username = a.Username,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email
                    })
                    .FirstOrDefault();

                return foundRecord;
            }
            catch (Exception exception)
            {
                _log.LogError(exception, 
                    $"User.cs CheckForUserInDb({guid})", Enums.SeverityCode.High);
                return new UserModel() { ErrorFlag = true, Message = exception.Message };
            }
        }

        private UserModel AddUserToDb(UserModel userInfo)
        {
            try
            {
                var newRecord = new TblUser()
                {
                    StatusId = (int)Enums.UserStatus.New,
                    Guid = userInfo.GUID,
                    Email = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Username = userInfo.Username,
                    Created = DateTimeOffset.Now,
                    Active = true
                };

                _dbContext.TblUser.Add(newRecord);
                _dbContext.SaveChanges();

                userInfo.Id = newRecord.Id;

                _tracker.TrackAction(Enums.TrackingActionType.UserCreated, Enums.TableEntity.tblUser, userInfo.Id.ToString(), userInfo.GUID, JsonConvert.SerializeObject(newRecord), JsonConvert.SerializeObject(newRecord));
            }
            catch (Exception exception)
            {
                _log.LogError(exception, 
                    $"User.cs AddUserToDb({JsonConvert.SerializeObject(userInfo)})", Enums.SeverityCode.High);
                userInfo.ErrorFlag = true;
                userInfo.Message = exception.Message;
            }

            return userInfo;
        }

        private void LogUserLogin(UserModel userInfo, UserContextModel userContext)
        {
            try
            {
                var clientHelper = new ClientHelper(_dataContextProvider, _log);
                var client = clientHelper.GetClient(userContext);
                if (client.ErrorFlag)
                    return;

                var newRecord = new TblLogin()
                {
                    UserId = userInfo.Id,
                    ClientId = client.Id,
                    MethodId = (int) Enums.LoginMethod.B2C_1_SUSI,
                    Created = DateTimeOffset.Now
                };

                _dbContext.TblLogin.Add(newRecord);
                _dbContext.SaveChanges();

                _tracker.TrackAction(Enums.TrackingActionType.Login, Enums.TableEntity.tblLogin, newRecord.Id.ToString(), userInfo.GUID, JsonConvert.SerializeObject(newRecord));
            }
            catch (Exception exception)
            {
                _log.LogError(exception, 
                    $"User.cs LogUserLogin({JsonConvert.SerializeObject(userInfo)}, {JsonConvert.SerializeObject(userContext)})", Enums.SeverityCode.Medium);
                return;
            }
        }

        #endregion
    }
}
