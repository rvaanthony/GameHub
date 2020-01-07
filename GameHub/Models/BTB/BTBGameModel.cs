using GameHub.Classes;
using System;
using System.Collections.Generic;

namespace GameHub.Models.BTB
{
    public class BTBGameModel : BaseModel
    {
        public int Id { get; set; }
        public string GameGuid { get; set; }
        public int UserId { get; set; }
        public Enums.BTBStatusType Status { get; set; }
        public int StatusId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
        public List<BTBGameCaseModel> Cases { get; set; }
    }
}
