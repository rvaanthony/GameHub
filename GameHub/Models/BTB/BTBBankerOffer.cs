namespace GameHub.Models.BTB
{
    public class BTBBankerOffer : BaseModel
    {
        public int Id { get; set; }
        public bool NewOffer { get; set; }
        public double Amount { get; set; }
        public int GameId { get; set; }
    }
}
