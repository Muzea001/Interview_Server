using Interview_Server.Models;

public class UserInterview
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }
    public int InterviewId { get; set; }
    public Interview Interview { get; set; }
    public int InterviewerId { get; set; }
    public User Interviewer { get; set; }
    public List<User> Applicants { get; set; } = new List<User>();
    public DateTime InterviewTime { get; set; }
    public int DurationInMinutes { get; set; }
    public InterviewStatus Status { get; set; }
    public List<Note> Notes { get; set; } = new List<Note>();
}


public enum InterviewStatus
{
    Scheduled,
    Completed,
    Canceled,
    AwaitingFeedback
}

