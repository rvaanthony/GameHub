using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblToken
    {
        public TblToken()
        {
            TblApicallLog = new HashSet<TblApicallLog>();
        }

        public int Id { get; set; }
        public string Guid { get; set; }
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public byte TypeId { get; set; }
        public int StatusId { get; set; }
        public int? ExpiresIn { get; set; }
        public DateTimeOffset ExpirationDateTime { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool Active { get; set; }

        public virtual TblStatus Status { get; set; }
        public virtual TblTokenType Type { get; set; }
        public virtual TblUser User { get; set; }
        public virtual ICollection<TblApicallLog> TblApicallLog { get; set; }
    }
}
