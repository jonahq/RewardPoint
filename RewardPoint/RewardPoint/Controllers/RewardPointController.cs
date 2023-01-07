using RewardPoint.Infrastructure.Services;
using RewardPoint.Core.Services;
using RewardPoint.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace RewardPoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RewardPointController : ControllerBase
    {
        private readonly IRewardService ireward;
        public RewardPointController(IRewardService rewardserv) { 
            ireward = rewardserv;
        }

        [HttpGet]
        [Route("reward")]
        public ActionResult<List<RewardModel>> GetReward(int userID, int timeRange)
        {
            List<RewardModel> output =  ireward.CalculatePoints(userID, timeRange);
            
            return Ok(output);
        }
    }
}