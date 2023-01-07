using RewardPoint.Core.Models;
using RewardPoint.Core.Services;
using RewardPoint.Core.Repositories;

namespace RewardPoint.Infrastructure.Services
{
    public class RewardServices : IRewardService
    {
        private readonly ITransactionRepo _transactionRepo;

        public RewardServices(ITransactionRepo trans) { _transactionRepo = trans; }
        //Given the amount of money, it converts to the reward point
        public int ConvertReward(int totalAmount)
        {
            int points = 0;
            if (totalAmount > 100) { points = 50 + (totalAmount - 100) * 2; }
            else if (totalAmount > 50) { points = totalAmount - 50; }
            return points;
        }


       //Calculate reward points for each months and later
       //being sent through response for the http call
        public List<RewardModel> CalculatePoints(int userID, int timeRange) {
            List<RewardModel> output = new List<RewardModel>();
            DateTime time_now = DateTime.Now;
            int total = 0;
            //Default output, assume each month has 0 points 
            for (int i = 0; i < timeRange; i++)
            {
                output.Add(new RewardModel() { 
                    Month = time_now.AddMonths(-i).ToString("yyyy, MMMM"), RewardPoint = 0 });
            }
            output.Add(new RewardModel() { Month = "Total", RewardPoint = total});
            //get all transaction records
            var UserDict = _transactionRepo.getTransRecord();
            if (UserDict.ContainsKey(userID)) {
                //if the user id is valid, get its list of transaction
                List<TransactionModel> trans_record = UserDict[userID];
                foreach (var elem in trans_record)
                {
                    int month_diff = (time_now.Year - elem.Date.Year) * 12 +
                    time_now.Month - elem.Date.Month;
                    //validate the time for the particular transaction is valid
                    //then add the money toward the element indexed by the corresponding month
                    if (month_diff < timeRange && month_diff > -1)
                    {
                        output[month_diff].RewardPoint += Convert.ToInt16(elem.Quantity * elem.ItemPrice);
                    }
                }
                //Convert all the money for each month to reward points
                for(int i = 0; i < output.Count; i++)
                {
                    int month_reward = output[i].RewardPoint;
                    output[i].RewardPoint = ConvertReward(month_reward);
                    total += month_reward;
                }
                //total reward points at the end
                output[output.Count - 1].RewardPoint = total;
            }
        

            return output;
        }


    }
        
        
    
}
