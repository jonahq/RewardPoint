using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardPoint.Core.Entities
{
    public class CustomerRecord
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public int RewardPoints { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
