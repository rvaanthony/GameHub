using System.Collections.Generic;

namespace GameHub.Models.BTB
{
    public class BTBCaseAmountModel : BaseModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public bool Active { get; set; }
    }

    public class BTBCaseAmountListModel : BaseModel
    {
        public List<BTBCaseAmountModel> List { get; set; }
    }
}
