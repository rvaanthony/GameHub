using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblTracking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TrackingActionId { get; set; }
        public int? ClientId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int? TableEntityId { get; set; }
        public string SourceId { get; set; }
        public DateTimeOffset Created { get; set; }

        public virtual TblClient Client { get; set; }
        public virtual TblTableEntity TableEntity { get; set; }
        public virtual TblTrackingAction TrackingAction { get; set; }
        public virtual TblUser User { get; set; }
    }
}
