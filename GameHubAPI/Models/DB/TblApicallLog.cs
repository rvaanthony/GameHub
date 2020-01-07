using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblApicallLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TokenId { get; set; }
        public byte MethodTypeId { get; set; }
        public string Url { get; set; }
        public string Data { get; set; }
        public string Response { get; set; }
        public int? ResponseStatusCodeId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }

        public virtual TblHttpMethodType MethodType { get; set; }
        public virtual TblHttpStatusCode ResponseStatusCode { get; set; }
        public virtual TblToken Token { get; set; }
        public virtual TblUser User { get; set; }
    }
}
