using RewardPoint.Core.Models;

namespace RewardPoint.Core.Services
{
    public interface IRewardService
    {
        public List<RewardModel> CalculatePoints(int userID, int timeRange);
    }
}
