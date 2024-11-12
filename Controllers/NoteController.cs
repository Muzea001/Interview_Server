using Interview_Server.Interfaces;
using Interview_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Interview_Server.Controllers
{
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IRepository<Note> _NoteRepository;

        public NoteController(IRepository<Note> repository)
        {
            _NoteRepository = repository;
        }

    }
}
