using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblPlayFabUser
    {
        public TblPlayFabUser()
        {
            TblPlayFabToken = new HashSet<TblPlayFabToken>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string PlayFabId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool Active { get; set; }

        public virtual TblUser User { get; set; }
        public virtual ICollection<TblPlayFabToken> TblPlayFabToken { get; set; }
    }
}
