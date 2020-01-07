using GameHub.Classes;

namespace GameHubAPI.Interfaces
{
    public interface ITracker
    {
        void TrackAction(Enums.TrackingActionType trackingActionType, Enums.TableEntity? entity, string sourceId, string userId, string newValue, string oldValue = null);
    }
}
