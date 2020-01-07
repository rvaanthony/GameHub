using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblHttpMethodType
    {
        public TblHttpMethodType()
        {
            TblApicallLog = new HashSet<TblApicallLog>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<TblApicallLog> TblApicallLog { get; set; }
    }
}
