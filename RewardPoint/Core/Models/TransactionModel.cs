namespace RewardPoint.Core.Models
{
    //object for the record of transaction
    public class TransactionModel
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int ItemId { get; set; }
        public double ItemPrice { get; set; }
        public int Quantity { get; set; }

    }

}