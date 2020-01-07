using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblBtbgameLog
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public byte ActionId { get; set; }
        public int UserId { get; set; }
        public int? TableEntityId { get; set; }
        public string SourceId { get; set; }
        public DateTimeOffset Created { get; set; }

        public virtual TblBtbactionType Action { get; set; }
        public virtual TblBtbgame Game { get; set; }
        public virtual TblTableEntity TableEntity { get; set; }
        public virtual TblUser User { get; set; }
    }
}
