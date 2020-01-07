using System.Collections.Generic;

namespace GameHub.Models.BTB
{
    public class BTBGameCaseModel : BaseModel
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public int CaseAmountId { get; set; }
        public int CaseNumber { get; set; }
        public int Amount { get; set; }
        public int GameId { get; set; }
        public string GameGuid { get; set; }
        public bool GameOver { get; set; }
    }

    public class BTBGameCasesModel : BaseModel
    {
        public List<BTBGameCaseModel> Cases { get; set; }
    }
}
