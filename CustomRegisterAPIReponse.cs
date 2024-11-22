using Interview_Server.Models;

namespace Interview_Server
{
    public class CustomRegisterAPIReponse
    {
        public User user { get; set; }

        public string Token { get; set; }
    }
}
