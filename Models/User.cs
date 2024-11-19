namespace Interview_Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } 
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<UserInterview> UserInterviews { get; set; } = new List<UserInterview>(); 
        public int LogbookId { get; set; }
        public Logbook Logbook { get; set; }

        public byte[] ProfileImage { get; set; }
    }
}
