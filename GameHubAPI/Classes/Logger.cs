using GameHub.Classes;
using GameHub.Extensions;
using GameHubAPI.Interfaces;
using GameHubAPI.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace GameHubAPI.Classes
{
    public class Logger : ILog
    {
        #region Properties

        private readonly GameHubContext _dbContext;
        public string CorrelationId { get; set; }

        public Logger(IDataContextProvider dataContextProvider)
        {
            _dbContext = dataContextProvider.GetGameHubContext();
        }

        #endregion

        public void LogInfo(string message, string info, string userId = null)
        {
            try
            {
                if (CorrelationId == null)
                    CorrelationId = Guid.NewGuid().ToString();
                var dateTime = DateTimeOffset.Now;
                var strPath = Directory.GetCurrentDirectory() + $"\\Log\\Log_{dateTime.Year}_{dateTime.Month}_{dateTime.Day}.txt";
                if (!File.Exists(strPath))
                    File.Create(strPath).Dispose();
                using (var sw = new StreamWriter(strPath, true))
                {
                    sw.WriteLine($"CorrelationId: {CorrelationId}" +
                                 $"{Environment.NewLine} Info: " + info +
                                 $"{Environment.NewLine} Message: {message}" + 
                                 $"{Environment.NewLine} UserId: {userId}");
                }
            }
            catch (Exception exception)
            {
                //TODO
            }
        }

        public void LogError(Exception exception, string info, Enums.SeverityCode severityCode = Enums.SeverityCode.Low, int? userId = null)
        {
            try
            {
                RejectChangesAsync();
                var exceptionDetails = exception.ToDetailedString();
                var innerException = exception.InnerException;
                var newRecord = new TblErrorLog()
                {
                    Exception = exceptionDetails,
                    Message = innerException?.Message,
                    Info = info,
                    SeverityCode = (int) severityCode,
                    Created = DateTimeOffset.Now
                };

                if (userId != null && userId != 0)
                    newRecord.UserId = userId;

                _dbContext.TblErrorLog.Add(newRecord);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        private void RejectChangesAsync()
        {
            var oldBehavior = _dbContext.ChangeTracker.QueryTrackingBehavior;
            var oldAutoDetect = _dbContext.ChangeTracker.AutoDetectChangesEnabled;

            // this is the key - disable change tracking logic so EF does not check that there were exception in on of tracked entities
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            var entries = _dbContext.ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Reload();
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; //Revert changes made to deleted entity.
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }

            _dbContext.ChangeTracker.QueryTrackingBehavior = oldBehavior;
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = oldAutoDetect;
        }
    }
}

