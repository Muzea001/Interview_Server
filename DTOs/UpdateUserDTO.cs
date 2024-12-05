using System.ComponentModel.DataAnnotations;

namespace Interview_Server.DTOs
{
    public class UpdateUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ProfileImage { get; set; }

    }
}
