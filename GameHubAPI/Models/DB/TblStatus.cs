using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblStatus
    {
        public TblStatus()
        {
            TblBtbgame = new HashSet<TblBtbgame>();
            TblBtbgameBankerOffer = new HashSet<TblBtbgameBankerOffer>();
            TblBtbgameCase = new HashSet<TblBtbgameCase>();
            TblBtbgameChosenCase = new HashSet<TblBtbgameChosenCase>();
            TblToken = new HashSet<TblToken>();
            TblUser = new HashSet<TblUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte TypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool? Active { get; set; }

        public virtual TblStatusType Type { get; set; }
        public virtual ICollection<TblBtbgame> TblBtbgame { get; set; }
        public virtual ICollection<TblBtbgameBankerOffer> TblBtbgameBankerOffer { get; set; }
        public virtual ICollection<TblBtbgameCase> TblBtbgameCase { get; set; }
        public virtual ICollection<TblBtbgameChosenCase> TblBtbgameChosenCase { get; set; }
        public virtual ICollection<TblToken> TblToken { get; set; }
        public virtual ICollection<TblUser> TblUser { get; set; }
    }
}
