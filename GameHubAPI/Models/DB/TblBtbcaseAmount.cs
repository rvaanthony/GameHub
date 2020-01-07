using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblBtbcaseAmount
    {
        public TblBtbcaseAmount()
        {
            TblBtbgameCase = new HashSet<TblBtbgameCase>();
        }

        public int Id { get; set; }
        public int Amount { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<TblBtbgameCase> TblBtbgameCase { get; set; }
    }
}
