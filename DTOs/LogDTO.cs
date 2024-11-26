using Interview_Server.Models;

namespace Interview_Server.DTOs
{
    public class LogDTO
    {
        public string title { get; set; }
        public string content { get; set; }
        public List<LogLabel>? label { get; set; } 

        public int interviewId { get; set; }
    }
}
