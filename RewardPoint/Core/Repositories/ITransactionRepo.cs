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
        //Add one transaction record, since we don't use the database just for
        //this assessment
        public void AddTransaction(TransactionModel tmod);
        //Get all transaction records
        public Dictionary<int, List<TransactionModel>> getTransRecord();
    }
}
