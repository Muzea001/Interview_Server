using Interview_Server.Models;

namespace Interview_Server.DTOs
{
    public class GetNoteDTO
    {

        public int Id { get; set; }
        public int UserInterviewId { get; set; }
        public string title { get; set; }

        public string content { get; set; }

        public NoteStatus status { get; set; }
    }
}
