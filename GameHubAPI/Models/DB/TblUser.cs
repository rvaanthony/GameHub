using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblApicallLog = new HashSet<TblApicallLog>();
            TblBtbgame = new HashSet<TblBtbgame>();
            TblBtbgameLog = new HashSet<TblBtbgameLog>();
            TblErrorLog = new HashSet<TblErrorLog>();
            TblLogin = new HashSet<TblLogin>();
            TblPlayFabUser = new HashSet<TblPlayFabUser>();
            TblToken = new HashSet<TblToken>();
            TblTracking = new HashSet<TblTracking>();
        }

        public int Id { get; set; }
        public string Guid { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool Active { get; set; }

        public virtual TblStatus Status { get; set; }
        public virtual ICollection<TblApicallLog> TblApicallLog { get; set; }
        public virtual ICollection<TblBtbgame> TblBtbgame { get; set; }
        public virtual ICollection<TblBtbgameLog> TblBtbgameLog { get; set; }
        public virtual ICollection<TblErrorLog> TblErrorLog { get; set; }
        public virtual ICollection<TblLogin> TblLogin { get; set; }
        public virtual ICollection<TblPlayFabUser> TblPlayFabUser { get; set; }
        public virtual ICollection<TblToken> TblToken { get; set; }
        public virtual ICollection<TblTracking> TblTracking { get; set; }
    }
}
