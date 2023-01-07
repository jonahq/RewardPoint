using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardPoint.Core.Entities
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

        public CustomerRecord Customer { get; set; }
    }
}
