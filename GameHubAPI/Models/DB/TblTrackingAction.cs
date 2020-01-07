using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblTrackingAction
    {
        public TblTrackingAction()
        {
            TblTracking = new HashSet<TblTracking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<TblTracking> TblTracking { get; set; }
    }
}
