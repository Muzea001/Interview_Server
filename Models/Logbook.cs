namespace Interview_Server.Models
{
    public class Logbook
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Title { get; set; }  

        public ICollection<Log> Logs { get; set; } = new List<Log>();

    }
}
