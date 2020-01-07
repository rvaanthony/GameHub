using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblTableEntity
    {
        public TblTableEntity()
        {
            TblBtbgameLog = new HashSet<TblBtbgameLog>();
            TblBtbgameResult = new HashSet<TblBtbgameResult>();
            TblTracking = new HashSet<TblTracking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<TblBtbgameLog> TblBtbgameLog { get; set; }
        public virtual ICollection<TblBtbgameResult> TblBtbgameResult { get; set; }
        public virtual ICollection<TblTracking> TblTracking { get; set; }
    }
}
