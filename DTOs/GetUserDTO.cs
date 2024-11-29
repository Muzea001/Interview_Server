using System.ComponentModel.DataAnnotations;

namespace Interview_Server.DTOs
{
    public class GetUserDTO
    {
        
        public string Username { get; set; }

        
        public string Password { get; set; }

        
        public string Email { get; set; }

        
        public string Mobile { get; set; }

        public string ProfileImage { get; set; }

        public int LoogbookId { get; set; }

    }
}
