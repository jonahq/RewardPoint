using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RewardPoint.Core.Models;

namespace RewardPoint.Core.Repositories
{
    public interface ITransactionRepo
    {
        public void AddTransaction(TransactionModel tmod);
        public Dictionary<int, List<TransactionModel>> getTransRecord();
    }
}
