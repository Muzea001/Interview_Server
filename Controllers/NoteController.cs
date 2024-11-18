using Interview_Server.DTOs;
using Interview_Server.Interfaces;
using Interview_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class NoteController : ControllerBase
    {

        private readonly IRepository<UserInterview> _userInterviewRepository;
        private readonly INote _noteRepository;

        public NoteController(INote noteRepository, IRepository<UserInterview> userInterviewRepository)
        {
            _noteRepository = noteRepository;
            _userInterviewRepository = userInterviewRepository;
        }

        [HttpGet("InterviewNotes/{userInterviewId}")]
        public async Task<IEnumerable<GetNoteDTO>> GetNotesByInterviewId(int userInterviewId)
        {
            try
            {

                Expression<Func<Note, bool>> predicate = ui => ui.UserInterviewId == userInterviewId;
                var notes = await _noteRepository.GetAllAsync();
                var getNoteDtos = notes.Select(n => new GetNoteDTO
                {
                    title = n.Title,
                    content = n.Content,
                    status = n.Status
                });
                Console.WriteLine("Query executed successfully, returning notes.");
                return getNoteDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching notes: {ex.Message}");
                throw;
            }
        }

        [HttpGet("Note/{noteId}")]
        public async Task<ActionResult<GetNoteDTO>> getNoteById(int noteId)
        {
            try
            {
                var note = await _noteRepository.GetByIdAsync(noteId);
                if (note == null)
                {
                    return BadRequest("No note found with this Id");
                }
                else
                {
                    var noteDTO = new GetNoteDTO
                    {
                        title = note.Title,
                        content = note.Content,
                        status = NoteStatus.NotReviewed
                    };
                    return Ok(noteDTO);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Note/{userInterviewId}")]

        public async Task<ActionResult> createNote(int userInterviewId, CreateNoteDTO note)
        {
            try { 
            Note newNote = new Note
            {
                UserInterviewId = userInterviewId,
                UserInterview = await _userInterviewRepository.GetByIdAsync(userInterviewId),
                Title = note.title,
                Content = note.content,
                Status = NoteStatus.NotReviewed
            };
            await _noteRepository.AddAsync(newNote);
            return CreatedAtAction(nameof(getNoteById), new { noteId = newNote.Id }, newNote);  
        }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{noteId}")]
        public async Task<ActionResult> editNote(int noteId, CreateNoteDTO note)
        {
            try
            {
                var noteToEdit = await _noteRepository.GetByIdAsync(noteId);
                if (noteToEdit == null)
                {
                    return NotFound("Note not found");
                }
                noteToEdit.Title = note.title;
                noteToEdit.Content = note.content;
                await _noteRepository.EditAsync(noteToEdit);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("changeStatus/{noteId}")]

        public async Task<ActionResult> ChangeNoteStatus(int noteId, [FromBody] string newStatus)
        {
            try
            {
                if (!Enum.TryParse(typeof(NoteStatus), newStatus, true, out var parsedStatus))
                {
                    return BadRequest("Invalid status");
                }
                var note = await _noteRepository.GetByIdAsync(noteId);
                if (note == null)
                {
                    return NotFound("Note not found");
                }
                await _noteRepository.ChangeStatusAsync(noteId, (NoteStatus)parsedStatus);
                return Ok(new
                {
                    message = "Note status updated successfully",
                    noteId = noteId,
                    newStatus = parsedStatus
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{noteId}")]
        public async Task<ActionResult> deleteNote(int noteId)
        {
            try
            {
                var note = await _noteRepository.GetByIdAsync(noteId);
                if (note == null)
                {
                    return NotFound("Note not found");
                }
                 await _noteRepository.deleteAsync(noteId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
