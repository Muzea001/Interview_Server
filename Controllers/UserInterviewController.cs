using Interview_Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("id/[Controller]")]
    public class UserInterviewController : ControllerBase
    {
        private readonly IRepository<UserInterview> _userInterviewRepository;
        public UserInterviewController(IRepository<UserInterview> userInterviewRepository)
        {
            _userInterviewRepository = userInterviewRepository;
        }

        [HttpGet("{userId:int}/interviews")]
        public async Task<ActionResult> getAllUserInterviewsNyUserId(int userId)
        {
            Expression<Func<UserInterview, bool>> predicate = ui => ui.UserId == userId;
            Expression<Func<UserInterview, object>> includeInterview = ui => ui.Interview;

            var userInterviews = await _userInterviewRepository.FindAsync(predicate, includeInterview);
            return Ok(userInterviews);
        }

        [HttpGet("{interviewId}")]
        public async Task<ActionResult> getUserInterviewById(int userInterviewId)
        {
            Expression<Func<UserInterview, object>> includeInterview = ui => ui.Interview;
            var interview = _userInterviewRepository.GetByIdAsync(userInterviewId, includeInterview);
            if(interview == null)
            {
                return NotFound("Interview with that id doesnt exist");
            }
            return Ok(interview);
        }
    }
}
