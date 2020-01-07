using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblPlayFabToken
    {
        public int Id { get; set; }
        public int PlayFabUserId { get; set; }
        public string SessionTicket { get; set; }
        public string EntityToken { get; set; }
        public DateTimeOffset TokenExperation { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool Active { get; set; }

        public virtual TblPlayFabUser PlayFabUser { get; set; }
    }
}
