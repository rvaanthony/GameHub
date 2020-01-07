using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblBtbresultType
    {
        public TblBtbresultType()
        {
            TblBtbgameResult = new HashSet<TblBtbgameResult>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<TblBtbgameResult> TblBtbgameResult { get; set; }
    }
}
