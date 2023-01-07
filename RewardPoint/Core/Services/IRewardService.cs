using RewardPoint.Core.Models;

namespace RewardPoint.Core.Services
{
    public interface IRewardService
    {
        //returns a list of reward points for different months and total points
        public List<RewardModel> CalculatePoints(int userID, int timeRange);
    }
}
