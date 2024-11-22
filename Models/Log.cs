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

        public List<LogLabel>? Label { get; set; }


    }


    public enum LogLabel
    {
        Technical,
        SoftSkill,
        Behavioral,
        Managerial,
        Coding,
        Design,
        Testing,
        Documentation,
        Research,
        Presentation
    }

}
