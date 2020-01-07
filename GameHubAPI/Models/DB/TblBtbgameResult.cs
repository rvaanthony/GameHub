using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblBtbgameResult
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public byte ResultId { get; set; }
        public int AmountWon { get; set; }
        public int TableEntityId { get; set; }
        public string SourceId { get; set; }
        public DateTimeOffset Created { get; set; }

        public virtual TblBtbgame Game { get; set; }
        public virtual TblBtbresultType Result { get; set; }
        public virtual TblTableEntity TableEntity { get; set; }
    }
}
