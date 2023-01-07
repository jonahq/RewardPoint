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
        //takes ID from the user and the past N months as timeRange
        //counting from the current month
        //For the assessment we can just use 3 
        public ActionResult<List<RewardModel>> GetReward(int userID, int timeRange)
        {
            //calls the method to give a list of months with their respective
            //reward points as well as the total points 
            List<RewardModel> output =  ireward.CalculatePoints(userID, timeRange);

            //return error message
            if(output == null || !output.Any()) { return NotFound(); }
            
            return Ok(output);
        }
    }
}