using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblLoginMethod
    {
        public TblLoginMethod()
        {
            TblLogin = new HashSet<TblLogin>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<TblLogin> TblLogin { get; set; }
    }
}
