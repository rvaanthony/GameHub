using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblBtbgame
    {
        public TblBtbgame()
        {
            TblBtbgameBankerOffer = new HashSet<TblBtbgameBankerOffer>();
            TblBtbgameCase = new HashSet<TblBtbgameCase>();
            TblBtbgameLog = new HashSet<TblBtbgameLog>();
            TblBtbgameResult = new HashSet<TblBtbgameResult>();
        }

        public int Id { get; set; }
        public string Guid { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }

        public virtual TblStatus Status { get; set; }
        public virtual TblUser User { get; set; }
        public virtual ICollection<TblBtbgameBankerOffer> TblBtbgameBankerOffer { get; set; }
        public virtual ICollection<TblBtbgameCase> TblBtbgameCase { get; set; }
        public virtual ICollection<TblBtbgameLog> TblBtbgameLog { get; set; }
        public virtual ICollection<TblBtbgameResult> TblBtbgameResult { get; set; }
    }
}
