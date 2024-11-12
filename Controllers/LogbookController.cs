

using Interview_Server.Interfaces;
using Interview_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogbookController : ControllerBase
    {
        private readonly IRepository<Logbook> _LogbookRepository;

        public LogbookController(IRepository<Logbook> repository)
        {
            _LogbookRepository = repository;
        }
    }
}
