using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblBtbgameCase
    {
        public TblBtbgameCase()
        {
            TblBtbgameChosenCase = new HashSet<TblBtbgameChosenCase>();
        }

        public int Id { get; set; }
        public string Guid { get; set; }
        public int GameId { get; set; }
        public int CaseAmountId { get; set; }
        public int CaseNumber { get; set; }
        public int StatusId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }

        public virtual TblBtbcaseAmount CaseAmount { get; set; }
        public virtual TblBtbgame Game { get; set; }
        public virtual TblStatus Status { get; set; }
        public virtual ICollection<TblBtbgameChosenCase> TblBtbgameChosenCase { get; set; }
    }
}
