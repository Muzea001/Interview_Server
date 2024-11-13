using Interview_Server.Interfaces;
using Interview_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class InterviewController : ControllerBase
    {
        private readonly IRepository<Interview> _interviewRepository;
        public InterviewController(IRepository<Interview> interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Interview>> GetAllInterviewsAsync()
        {
            try
            {
                var interviews = await _interviewRepository.GetAllAsync();
                Console.WriteLine("Query executed successfully, returning interviews.");
                return interviews;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching interviews: {ex.Message}");
                throw;
            }
        }


        [HttpGet("{interviewId}")]
        public async Task<ActionResult<Interview>> getInterviewById(int id)
        {
            var interview = await _interviewRepository.GetByIdAsync(id);
            if (interview == null)
            {
                return BadRequest("No interview found with this Id");
            }
            return Ok(interview);
        }

        [HttpPost]
        public async Task<ActionResult<Interview>> createInterview(Interview interview)
        {
            var createdInterview = await _interviewRepository.AddAsync(interview);
            return CreatedAtAction(nameof(getInterviewById), new { id = interview.InterviewId }, interview);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Interview>> editInterview(int id, Interview interview)
        {
            if(id != interview.InterviewId)
            {
                return BadRequest("Incorrect id passed");
            }
            await _interviewRepository.EditAsync(interview);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Interview>> deleteUser(int id)
        {
            if(id == null)
            {
                return BadRequest("Id invalid");
            }
            var interview = await _interviewRepository.GetByIdAsync(id);
            if (interview == null)
            {
                return NotFound("Interview with this id not found");
            }
            _interviewRepository.deleteAsync(interview.InterviewId);
            return Ok();
        }

    }
}
