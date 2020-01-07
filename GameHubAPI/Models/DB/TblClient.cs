using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblClient
    {
        public TblClient()
        {
            TblLogin = new HashSet<TblLogin>();
            TblTracking = new HashSet<TblTracking>();
        }

        public int Id { get; set; }
        public int Osid { get; set; }
        public int BrowserId { get; set; }
        public int DeviceTypeId { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public bool Crawler { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool Active { get; set; }

        public virtual TblBrowser Browser { get; set; }
        public virtual TblDeviceType DeviceType { get; set; }
        public virtual TblOs Os { get; set; }
        public virtual ICollection<TblLogin> TblLogin { get; set; }
        public virtual ICollection<TblTracking> TblTracking { get; set; }
    }
}
