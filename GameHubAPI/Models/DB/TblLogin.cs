using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblLogin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public int MethodId { get; set; }
        public DateTimeOffset Created { get; set; }

        public virtual TblClient Client { get; set; }
        public virtual TblLoginMethod Method { get; set; }
        public virtual TblUser User { get; set; }
    }
}
