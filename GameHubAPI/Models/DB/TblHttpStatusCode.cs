using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblHttpStatusCode
    {
        public TblHttpStatusCode()
        {
            TblApicallLog = new HashSet<TblApicallLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<TblApicallLog> TblApicallLog { get; set; }
    }
}
