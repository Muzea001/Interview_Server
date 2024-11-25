using Interview_Server.DTOs;
using Interview_Server.Interfaces;
using Interview_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LogbookController : ControllerBase
    {
        private readonly IRepository<Logbook> _logbookRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Log> _logRepository;

        public LogbookController(IRepository<Logbook> logbookRepository, IRepository<User> userRepository, IRepository<Log> logRepository)
        {
            _logbookRepository = logbookRepository;
            _userRepository = userRepository;
            _logRepository = logRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<LogBookDTO>> GetAllLogbooks()
        {
            try
            {

                var logbooks = await _logbookRepository.GetAllAsync(logbook => logbook.Logs);
                var logbookDtos = logbooks.Select(lb => new LogBookDTO
                {
                    title = lb.Title,
                    logs = lb.Logs.Select(l => new LogDTO
                    {
                        title = l.Title,
                        content = l.Content,
                        label = l.Label
                    }).ToList()
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
        public async Task<ActionResult<Logbook>> GetLogbookById(int logbookId)
        {
            try
            {
                var logbook = await _logbookRepository.GetByIdAsync(logbookId, logbook => logbook.Logs);
                if (logbook == null)
                {
                    return NotFound("Logbook not found");
                }

                var logbookDTO = new LogBookDTO
                {
                    title = logbook.Title,
                    logs = logbook.Logs.Select(a => new LogDTO
                    {
                        content = a.Content,
                        title = a.Title,
                        label = a.Label,    
                    }).ToList()
                };
                return Ok(logbook);
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
                var logbookToEdit = await _logbookRepository.GetByIdAsync(logbookId, logbook => logbook.Logs);
                if (logbookToEdit == null)
                {
                    return NotFound("Logbook not found");
                }

                logbookToEdit.Title = logbookDto.title;

                // Update logs and their labels
                foreach (var logDto in logbookDto.logs)
                {
                    var logToEdit = logbookToEdit.Logs.FirstOrDefault(l => l.Title == logDto.title && l.Content == logDto.content);
                    if (logToEdit != null)
                    {
                        logToEdit.Label = logDto.label;
                    }
                    else
                    {
                        logbookToEdit.Logs.Add(new Log
                        {
                            Title = logDto.title,
                            Content = logDto.content,
                            Label = logDto.label,
                            LogbookId = logbookId
                        });
                    }
                }

                await _logbookRepository.EditAsync(logbookToEdit);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("logs/{logId}/labels")]
        public async Task<ActionResult> UpdateLogLabels(int logId, UpdateLogLabelsDTO updateLogLabelsDto)
        {
            try
            {
                var logToUpdate = await _logRepository.GetByIdAsync(logId);
                if (logToUpdate == null)
                {
                    return NotFound("Log not found");
                }

                logToUpdate.Label = updateLogLabelsDto.Labels;

                await _logRepository.EditAsync(logToUpdate);
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
