namespace Interview_Server.Models
{
    public class Note
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int UserInterviewId { get; set; }

        public UserInterview UserInterview { get; set; }

        public string Status { get; set; }

    }
}
