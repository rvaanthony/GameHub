using System;
using System.Collections.Generic;

namespace GameHubAPI.Models.DB
{
    public partial class TblBtbgameBankerOffer
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int OfferAmount { get; set; }
        public int StatusId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }

        public virtual TblBtbgame Game { get; set; }
        public virtual TblStatus Status { get; set; }
    }
}
