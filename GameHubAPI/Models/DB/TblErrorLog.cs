using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblErrorLog
    {
        public int Id { get; set; }
        public string Exception { get; set; }
        public string Message { get; set; }
        public string Info { get; set; }
        public int SeverityCode { get; set; }
        public int? UserId { get; set; }
        public DateTimeOffset Created { get; set; }

        public virtual TblUser User { get; set; }
    }
}
