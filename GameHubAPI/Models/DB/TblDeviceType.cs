using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblDeviceType
    {
        public TblDeviceType()
        {
            TblClient = new HashSet<TblClient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<TblClient> TblClient { get; set; }
    }
}
