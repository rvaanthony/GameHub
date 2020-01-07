using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblBtbgameChosenCase
    {
        public int Id { get; set; }
        public int GameCaseId { get; set; }
        public int StatusId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }

        public virtual TblBtbgameCase GameCase { get; set; }
        public virtual TblStatus Status { get; set; }
    }
}
