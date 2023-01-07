using RewardPoint.Core.Models;
using RewardPoint.Core.Services;
using RewardPoint.Core.Repositories;

namespace RewardPoint.Infrastructure.Repositories
{  
    public class TransactionRepo:ITransactionRepo
    {
        //Since we don't use database just for this assessment
        //a dictionary is used to store data for the demonstration
        //it maps the user's ID as key and the value is its list of transaction records
        private Dictionary<int, List<TransactionModel>> UserDict =
            new Dictionary<int, List<TransactionModel>>();

        //Add one transaction record toward the dictionary
        public void AddTransaction(TransactionModel tmod)
        {
            int key = tmod.UserId;
            if (UserDict.ContainsKey(key))
            {
                UserDict[key].Add(tmod);
            }
            else
            {
                UserDict.Add(key, new List<TransactionModel>() { tmod });
            }
        }
        //Add some default transaction
        public void InitTransaction()
        {
            AddTransaction(new TransactionModel()
            {
                Date = new DateTime(2023, 01, 02),
                Quantity = 1,
                ItemId = 10,
                ItemPrice = 30,
                UserId = 1
            });
            AddTransaction(new TransactionModel()
            {
                Date = new DateTime(2022, 11, 12),
                Quantity = 2,
                ItemId = 4,
                ItemPrice = 50,
                UserId = 1
            });
            AddTransaction(new TransactionModel()
            {
                Date = new DateTime(2022, 12, 02),
                Quantity = 1,
                ItemId = 8,
                ItemPrice = 120,
                UserId = 1
            });
            AddTransaction(new TransactionModel()
            {
                Date = new DateTime(2022, 10, 02),
                Quantity = 1,
                ItemId = 10,
                ItemPrice = 300,
                UserId = 1
            });
            AddTransaction(new TransactionModel()
            {
                Date = new DateTime(2022, 10, 02),
                Quantity = 1,
                ItemId = 10,
                ItemPrice = 300,
                UserId = 3
            });
        }
        public Dictionary<int, List<TransactionModel>> getTransRecord() 
        {
            return UserDict;
        }
    }
}
