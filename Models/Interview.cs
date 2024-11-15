namespace Interview_Server.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public List<UserInterview> UserInterviews { get; set; } = new List<UserInterview>();
    }
}
