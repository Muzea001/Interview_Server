namespace Interview_Server.Models
{
    public class Logbook
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int UserInterviewId { get; set; }

        public UserInterview UserInterview { get; set; }

        public User User  { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public TimeOnly Time { get; set; }

        
    }
}
