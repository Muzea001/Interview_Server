using Interview_Server.DTOs;
using Interview_Server.Interfaces;
using Interview_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LogbookController : ControllerBase
    {
        private readonly IRepository<Logbook> _logbookRepository;
        private readonly IRepository<User> _userRepository;

        public LogbookController(IRepository<Logbook> logbookRepository, IRepository<User> userRepository)
        {
            _logbookRepository = logbookRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<LogBookDTO>> GetAllLogbooks()
        {
            try
            {
                var logbooks = await _logbookRepository.GetAllAsync();
                var logbookDtos = logbooks.Select(lb => new LogBookDTO
                {
                    title = lb.Title,
                });
                return logbookDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching logbooks: {ex.Message}");
                throw;
            }
        }

        [HttpGet("{logbookId}")]
        public async Task<ActionResult<LogBookDTO>> GetLogbookById(int logbookId)
        {
            try
            {
                var logbook = await _logbookRepository.GetByIdAsync(logbookId);
                if (logbook == null)
                {
                    return NotFound("Logbook not found");
                }

                var logbookDTO = new LogBookDTO
                {
                    title = logbook.Title,
                };
                return Ok(logbookDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> CreateLogbook(int userId, LogBookDTO logbookDto)
        {
            try
            {
                Logbook newLogbook = new Logbook
                {
                    Title = logbookDto.title,
                    Logs = new List<Log>(),
                    UserId = userId,
                    User = _userRepository.GetByIdAsync(userId).Result
                    // Include other properties if needed
                };

                await _logbookRepository.AddAsync(newLogbook);
                return CreatedAtAction(nameof(GetLogbookById), new { logbookId = newLogbook.Id }, newLogbook);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{logbookId}")]
        public async Task<ActionResult> EditLogbook(int logbookId, LogBookDTO logbookDto)
        {
            try
            {
                var logbookToEdit = await _logbookRepository.GetByIdAsync(logbookId);
                if (logbookToEdit == null)
                {
                    return NotFound("Logbook not found");
                }

                logbookToEdit.Title = logbookDto.title;
                await _logbookRepository.EditAsync(logbookToEdit);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{logbookId}")]
        public async Task<ActionResult> DeleteLogbook(int logbookId)
        {
            try
            {
                var logbook = await _logbookRepository.GetByIdAsync(logbookId);
                if (logbook == null)
                {
                    return NotFound("Logbook not found");
                }

                await _logbookRepository.deleteAsync(logbookId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
