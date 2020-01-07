namespace GameHub.Models.PlayFab
{
    public class PlayFabVirtualCurrencyModel : BaseModel
    {
        public string VirtualCurrency { get; set; }
        public string PlayFabId { get; set; }
        public int BalanceChange { get; set; }
        public int Balance { get; set; }
        public int Amount { get; set; }
    }
}
