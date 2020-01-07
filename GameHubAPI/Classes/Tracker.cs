using System;
using System.Linq;
using GameHub.Classes;
using GameHubAPI.Interfaces;
using GameHubAPI.Models.DB;

namespace GameHubAPI.Classes
{
    public class Tracker : ITracker
    {
        #region Properties

        private readonly GameHubContext _dbContext;
        private readonly ILog _logger;

        public Tracker(IDataContextProvider dataContextProvider, ILog logger)
        {
            _dbContext = dataContextProvider.GetGameHubContext();
            _logger = logger;
        }

        #endregion

        public void TrackAction(Enums.TrackingActionType trackingActionType, Enums.TableEntity? entity, string sourceId, string userId,
            string newValue, string oldValue = null)
        {
            try
            {
                var newRecord = new TblTracking()
                {
                    UserId = _dbContext.TblUser.Where(a => a.Guid == userId).Select(a => a.Id).FirstOrDefault(),
                    TrackingActionId = (int) trackingActionType,
                    NewValue = newValue,
                    Created = DateTimeOffset.Now
                };

                if (oldValue != null)
                    newRecord.OldValue = oldValue;
                if (entity != null)
                    newRecord.TableEntityId = (int) entity;
                if (!string.IsNullOrEmpty(sourceId))
                    newRecord.SourceId = sourceId;

                _dbContext.TblTracking.Add(newRecord);
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Tracker.cs TrackAction({trackingActionType}, {entity}, {sourceId}. {userId}, {newValue}, {oldValue})");
            }
        }
    }
}
