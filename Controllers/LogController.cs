using Interview_Server.DTOs;
using Interview_Server.Interfaces;
using Interview_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LogController : ControllerBase
    {
        private readonly IRepository<Log> _logRepository;
        private readonly IRepository<Interview> _interviewRepository;
        private readonly IRepository<Logbook> _logbookRepository;

        public LogController(IRepository<Log> logRepository, IRepository<Interview> interviewRepository, IRepository<Logbook> logbookRepository)
        {
            _logRepository = logRepository;
            _interviewRepository = interviewRepository;
            _logbookRepository = logbookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<LogDTO>> getAllLogs()
        {
            try
            {
                var logs = await _logRepository.GetAllAsync();
                var logDTos = logs.Select(log => new LogDTO
                {
                    title = log.Title,
                    content = log.Content
                });
                return logDTos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching logs: {ex.Message}");
                throw;
            }
        }

        [HttpGet("/{companyName}")]
        public async Task<IEnumerable<LogDTO>> GetLogsByInterviewId(string companyName)
        {
            try
            {
                Expression<Func<Log, object>> predicate = log => log.Interview.CompanyName == companyName;
                var logs = await _logRepository.GetAllAsync(predicate);
                var logDtos = logs.Select(log => new LogDTO
                {
                    title = log.Title,
                    content = log.Content
                });
                Console.WriteLine("Query executed successfully, returning logs.");
                return logDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching logs: {ex.Message}");
                throw;
            }
        }

        [HttpGet("{logId}")]
        public async Task<ActionResult<LogDTO>> GetLogById(int logId)
        {
            try
            {
                var log = await _logRepository.GetByIdAsync(logId);
                if (log == null)
                {
                    return BadRequest("No log found with this Id");
                }

                var logDTO = new LogDTO
                {
                    title = log.Title,
                    content = log.Content
                };
                return Ok(logDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{interviewId}/{logbookId}")]
        public async Task<ActionResult> CreateLog(int interviewId, int logbookId, LogDTO logDto)
        {
            try
            {
                var userInterview = await _interviewRepository.GetByIdAsync(interviewId);
                if (userInterview == null)
                {
                    return NotFound("UserInterview not found");
                }
                Logbook logbook = await _logbookRepository.GetByIdAsync(logbookId);
                if(logbook == null)
                {
                    return NotFound("Logbook not found");
                }
                Log newLog = new Log
                {
                    InterviewId = interviewId,
                    Interview = userInterview,
                    Title = logDto.title,
                    Content = logDto.content,
                    LogbookId = logbookId,
                    Label = logDto.label,
                    Logbook =logbook
                    
                };

                await _logRepository.AddAsync(newLog);
                return CreatedAtAction(nameof(GetLogById), new { logId = newLog.Id }, newLog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{logId}")]
        public async Task<ActionResult> EditLog(int logId, LogDTO logDto)
        {
            try
            {
                var logToEdit = await _logRepository.GetByIdAsync(logId);
                var newInterview = await _interviewRepository.GetByIdAsync(logDto.interviewId);
                if (logToEdit == null)
                {
                    return NotFound("Log not found");
                }

                logToEdit.Title = logDto.title;
                logToEdit.Content = logDto.content;
                logToEdit.Interview = newInterview;
                logToEdit.InterviewId = newInterview.Id;
                logToEdit.Label = logDto.label;
                await _logRepository.EditAsync(logToEdit);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{logId}")]
        public async Task<ActionResult> DeleteLog(int logId)
        {
            try
            {
                var log = await _logRepository.GetByIdAsync(logId);
                if (log == null)
                {
                    return NotFound("Log not found");
                }

                await _logRepository.deleteAsync(logId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
