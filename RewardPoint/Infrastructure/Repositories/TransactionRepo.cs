using RewardPoint.Core.Models;
using RewardPoint.Core.Services;
using RewardPoint.Core.Repositories;

namespace RewardPoint.Infrastructure.Repositories
{  
    public class TransactionRepo:ITransactionRepo
    {
        private Dictionary<int, List<TransactionModel>> UserDict =
            new Dictionary<int, List<TransactionModel>>();
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
