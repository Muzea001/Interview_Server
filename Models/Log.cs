namespace Interview_Server.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int LogbookId { get; set; }
        public Logbook Logbook { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public int InterviewId { get; set; }
        public Interview Interview { get; set; }

    }

}
