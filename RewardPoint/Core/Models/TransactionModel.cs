namespace RewardPoint.Core.Models
{
    //object for the record of transaction
    public class TransactionModel
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int TransactionId { get; set; }
        public double ItemPrices { get; set; }

    }

}