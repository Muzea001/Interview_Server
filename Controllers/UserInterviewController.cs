using Interview_Server.DTOs;
using Interview_Server.Interfaces;
using Interview_Server.Models;
using Interview_Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("id/[Controller]")]
    public class UserInterviewController : ControllerBase
    {
        private readonly InterviewValidationService _validationservice;
        private readonly IRepository<UserInterview> _userInterviewRepository;
        private readonly IRepository<User> _UserRepository;
        private readonly IRepository<Interview> _InterviewRepository;
        private readonly IUserInterview _userInterview;
        private readonly InterviewValidationService _interviewValidationService;
        private readonly IHubContext<NotificationHub> _hubContext;  
        public UserInterviewController(IRepository<UserInterview> userInterviewRepository,
            IUserInterview userInterivew, IRepository<User> userRepository,
            IRepository<Interview> interviewRepository,
            InterviewValidationService intservice, IHubContext<NotificationHub> notificationHub)
        {
            _userInterviewRepository = userInterviewRepository;
            _UserRepository = userRepository;
            _InterviewRepository = interviewRepository;
            _userInterview = userInterivew;
            _interviewValidationService = intservice;
            _hubContext = notificationHub;
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
                status = ui.Status,
                
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

        [HttpPost("create-interview")]
        public async Task<ActionResult> createUserInterview(CreateInterviewDTO interview)
        {
            var errors = new List<String>();

            if (!_interviewValidationService.ValidateTitle(interview.title))
            {
                errors.Add("Invalid title. Title must be between 3 and 50 characters long");
            }

            if (!_interviewValidationService.ValidateDescription(interview.description))
            {
                errors.Add("Invalid description. Description must be between 5 and 500 characters long");
            }

            if (!_interviewValidationService.ValidateTime(interview.time))
            {
                errors.Add("Invalid time. Time must be in the future");
            }

            if (!_interviewValidationService.ValidateAddress(interview.address))
            {
                errors.Add("Invalid address. Address must be between 5 and 100 characters long");
            }

            if (!_interviewValidationService.ValidateDuration(interview.duration))
            {
                errors.Add("Invalid duration. Duration must be between 1 and 300 minutes");
            }

            if (!_interviewValidationService.ValidateCompanyName(interview.companyName))
            {
                errors.Add("Invalid company name. Company name must be between 3 and 50 characters long");
            }

            if (errors.Any())
            {
                return BadRequest(errors);
            }

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
            var notificationMessage = $"A new interview has been created with ID {userInterview.InterviewId}.";
            await _hubContext.Clients.All.SendAsync(notificationMessage);   


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

            userInterview.Status = (InterviewStatus)parsedStatus; 

            await _userInterviewRepository.EditAsync(userInterview);

            // hello
            var notificationMessage = $"Interview status for UserInterview ID {UserInterviewId} has changed to {newStatus}.";
            await _hubContext.Clients.Group("1")
                .SendAsync("ReceiveNotification", notificationMessage);
            return Ok(userInterview);
        }



        [HttpPut("{UserInterviewId}")]
        public async Task<ActionResult> updateUserInterview(int UserInterviewId, CreateInterviewDTO interview)
        {
            var errors = new List<String>();

            if (!_interviewValidationService.ValidateTitle(interview.title))
            {
                errors.Add("Invalid title. Title must be between 3 and 50 characters long");
            }

            if (!_interviewValidationService.ValidateDescription(interview.description))
            {
                errors.Add("Invalid description. Description must be between 5 and 500 characters long");
            }

            if (!_interviewValidationService.ValidateTime(interview.time))
            {
                errors.Add("Invalid time. Time must be in the future");
            }

            if (!_interviewValidationService.ValidateAddress(interview.address))
            {
                errors.Add("Invalid address. Address must be between 5 and 100 characters long");
            }

            if (!_interviewValidationService.ValidateDuration(interview.duration))
            {
                errors.Add("Invalid duration. Duration must be between 1 and 300 minutes");
            }

            if (!_interviewValidationService.ValidateCompanyName(interview.companyName))
            {
                errors.Add("Invalid company name. Company name must be between 3 and 50 characters long");
            }
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
            if (userInterview.Interview == null)
            {
                userInterview.Interview = new Interview();
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
