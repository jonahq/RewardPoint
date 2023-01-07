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
        public Dictionary<int, List<TransactionModel>> getTransRecord() 
        {
            return UserDict;
        }
    }
}
