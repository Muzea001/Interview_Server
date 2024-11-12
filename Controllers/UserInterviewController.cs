using Interview_Server.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserInterviewController : ControllerBase
    {

        private readonly IRepository<UserInterview> _UserInterviewRespository;

        public UserInterviewController(IRepository<UserInterview> repository)
        {

           _UserInterviewRespository = repository;

        }}

    }

