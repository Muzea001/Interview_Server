using Interview_Server.DTOs;
using Interview_Server.Interfaces;
using Interview_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("id/[Controller]")]
    public class UserInterviewController : ControllerBase
    {
        private readonly IRepository<UserInterview> _userInterviewRepository;
        private readonly IRepository<User> _UserRepository;
        private readonly IRepository<Interview> _InterviewRepository;
        private readonly IUserInterview _userInterview;
        public UserInterviewController(IRepository<UserInterview> userInterviewRepository,IUserInterview userInterivew, IRepository<User> userRepository, IRepository<Interview> interviewRepository)
        {
            _userInterviewRepository = userInterviewRepository;
            _UserRepository = userRepository;
            _InterviewRepository = interviewRepository;
            _userInterview = userInterivew;
        }

        [HttpGet("{userId:int}/interviews")]
        public async Task<ActionResult> getAllUserInterviewsNyUserId(int userId)
        {
            Expression<Func<UserInterview, bool>> predicate = ui => ui.UserId == userId;
            Expression<Func<UserInterview, object>> includeInterview = ui => ui.Interview;

            var userInterviews = await _userInterviewRepository
            .FindAsync(ui => ui.UserId == userId, includeInterview);
            var getInterviewDTOS = userInterviews.Select(ui => new GetInterviewDTO
            {
                Id = ui.Id,
                companyName = ui.Interview.CompanyName,
                title = ui.Interview.Title,
                address = ui.Interview.Address,
                description = ui.Interview.Description,
                duration = ui.DurationInMinutes,
                time = ui.InterviewTime,
                notes = ui.Notes,
                status = InterviewStatus.AwaitingFeedback
                
            }); 
            return Ok(getInterviewDTOS);
        }

        [HttpGet("Search")]
        public async Task<ActionResult> SearchUserInterviews([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty");
            }
            Expression<Func<UserInterview, object>> includeInterview = ui => ui.Interview;
            var results = await _userInterview.SearchUserInterviewsAsync(searchTerm, includeInterview);
            if (!results.Any())
            {
                return NotFound("No matches found.");
            }
            var resultDTOs = results.Select(interview => new GetInterviewDTO
            {
                Id = interview.Id,
                companyName = interview.Interview.CompanyName,
                title = interview.Interview.Title,
                address = interview.Interview.Address,
                description = interview.Interview.Description,
                duration = interview.DurationInMinutes,
                time = interview.InterviewTime,
                notes = interview.Notes,
                status = interview.Status
            }).ToList();

            return Ok(resultDTOs);
        }

        [HttpGet("{UserInterviewId}")]
        public async Task<ActionResult> getUserInterviewById(int UserInterviewId)
        {
            Expression<Func<UserInterview, object>> includeInterview = ui => ui.Interview;
            var interview = await _userInterviewRepository.GetByIdAsync(UserInterviewId, includeInterview);
            if (interview == null)
            {
                return NotFound("Interview with that id doesnt exist");
            }
            var getInterviewDTO = new GetInterviewDTO
            {
                Id = interview.Id,
                companyName = interview.Interview.CompanyName,
                title = interview.Interview.Title,
                address = interview.Interview.Address,
                description = interview.Interview.Description,
                duration = interview.DurationInMinutes,
                time = interview.InterviewTime,
                notes = interview.Notes,
                status = InterviewStatus.AwaitingFeedback

            };

            return Ok(getInterviewDTO);
        }

        [HttpPost]
        public async Task<ActionResult> createUserInterview(CreateInterviewDTO interview)
        {
            var user = await _UserRepository.GetByIdAsync(1);
            if (user == null)
            {
                return NotFound("User not found");
            }

            Interview newInterview = new Interview
            {
                Title = interview.title,
                Description = interview.description,
                Address = interview.address,
                CompanyName = interview.companyName,
                UserInterviews = new List<UserInterview>()
                
            };

            await _InterviewRepository.AddAsync(newInterview);

            UserInterview userInterview = new UserInterview
            {
                User = user,
                UserId = user.Id,
                Status = InterviewStatus.AwaitingFeedback,
                Interview = newInterview,
                InterviewId = newInterview.Id,
                InterviewTime = interview.time.Value,
                DurationInMinutes = interview.duration,
                Notes = new List<Note>(),
                Role = UserRole.Interviewee
            };

            await _userInterviewRepository.AddAsync(userInterview);
            
            

            return CreatedAtAction(nameof(getUserInterviewById), new { UserInterviewId = userInterview.Id }, userInterview);
        }

        [HttpPut("changeStatus/{UserInterviewId}")]
        public async Task<ActionResult> updateInterviewStatus(int UserInterviewId, [FromBody] string newStatus)
        {
            if (!Enum.TryParse(typeof(InterviewStatus), newStatus, true, out var parsedStatus))
            {
                return BadRequest("Invalid status");
            }

            var userInterview = await _userInterviewRepository.GetByIdAsync(UserInterviewId); 
            if (userInterview == null)
            {
                return NotFound("UserInterview not found");
            }
            await _userInterview.ChangeStatusAsync(UserInterviewId, (InterviewStatus)parsedStatus);
            return Ok(userInterview);
        }



        [HttpPut("{UserInterviewId}")]
        public async Task<ActionResult> updateUserInterview(int UserInterviewId, CreateInterviewDTO interview)
        {
            var userInterview = await _userInterviewRepository.GetByIdAsync(UserInterviewId);
            if (userInterview == null)
            {
                return NotFound("UserInterview not found");
            }

            var user = await _UserRepository.GetByIdAsync(1);  
            if (user == null)
            {
                return NotFound("User not found");
            }

            userInterview.Interview.Title = interview.title ?? userInterview.Interview.Title;  
            userInterview.Interview.Description = interview.description ?? userInterview.Interview.Description;
            userInterview.Interview.Address = interview.address ?? userInterview.Interview.Address;
            userInterview.Interview.CompanyName = interview.companyName ?? userInterview.Interview.CompanyName;
            userInterview.InterviewTime = interview.time ?? userInterview.InterviewTime;            

            await _userInterviewRepository.EditAsync(userInterview);

            return Ok(userInterview);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInterview(int id)
        {
            var userInterview = await _userInterviewRepository.GetByIdAsync(id);
            if (userInterview == null)
            {
                return NotFound();
            }
            await _userInterviewRepository.deleteAsync(id);
            return NoContent();
        }

    }
}
