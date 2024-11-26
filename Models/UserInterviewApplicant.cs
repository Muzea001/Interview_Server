namespace Interview_Server.Models
{
    public class UserInterviewApplicant
    {
        public int Id { get; set; }

        public UserInterview UserInterview { get; set; }

        public int UserInterviewId { get; set; }

        public User Applicant { get; set; }

        public int ApplicantId { get; set; }

        public ApplicantStatus Status { get; set; }
    }

    public enum ApplicantStatus
    {
        Student, 
        Graduate,
        Professional,
        Other
    }
}
