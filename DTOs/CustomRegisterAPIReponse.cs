using Interview_Server.Models;

namespace Interview_Server.DTOs
{
    public class CustomRegisterAPIReponse
    {
        public GetUserDTO user { get; set; }

        public string Token { get; set; }
    }
}
