namespace Interview_Server.DTOs
{
    public class CreateInterviewDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public DateTime? time { get; set; }

        public string address { get; set; }

        public int duration { get; set; }

        public string companyName { get; set; }
    }
}
