
using Interview_Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public static class SeedData
{
    public static string HashPassword(string plainpassword)
    {
        var passwordHasher = new PasswordHasher<User>();
        return passwordHasher.HashPassword(null, plainpassword);
    }
}

public class DatabaseContext : DbContext
{
    private string _connectionString;
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>()
        .Property(l => l.Label)
        .HasConversion(
            v => string.Join(",", v.Select(x => x.ToString())), // Convert to comma-separated string
            v => v.Split(",", StringSplitOptions.RemoveEmptyEntries) // Convert back to a list
                .Select(x => Enum.Parse<LogLabel>(x))
                .ToList()
        );

        modelBuilder.Entity<Note>()
            .Property(n => n.Status)
            .HasConversion<string>();

        modelBuilder.Entity<UserInterview>()
            .Property(n => n.Status)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .HasMany(p => p.UserInterviews)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Interview>()
            .HasMany(p => p.UserInterviews)
            .WithOne(p => p.Interview)
            .HasForeignKey(p => p.InterviewId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserInterview>()
            .HasMany(p => p.Notes)
            .WithOne(p => p.UserInterview)
            .HasForeignKey(p => p.UserInterviewId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Log>()
            .HasOne(p => p.Logbook)
            .WithMany(p => p.Logs)
            .HasForeignKey(p => p.LogbookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Logbook>()
            .HasOne(p => p.User)
            .WithOne(p => p.Logbook)
            .HasForeignKey<Logbook>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        var usersWithImages = SeedImageForUsers();
        foreach (var user in usersWithImages)
        {
            modelBuilder.Entity<User>().HasData(user);
        }



        modelBuilder.Entity<Interview>().HasData(
            new Interview() { Id = 1, CompanyName = "PayEx", Title = "Technical Interview", Description = "Technical interview after a short speedinterview", Address = "Kongens gate 6, Oslo" },
            new Interview() { Id = 2, CompanyName = "Nordre Follo Kommune", Title = "Førstegangsintervju", Description = "Bli kjent intervju", Address = "Idrettsveien 8, Ski" },
            new Interview() { Id = 3, CompanyName = "TechCorp Solutions", Title = "Software Engineer Interview", Description = "A technical interview for a software engineer position", Address = "Helsingborgveien 5, Bergen" },
            new Interview() { Id = 4, CompanyName = "GlobalTech Innovations", Title = "Data Analyst Interview", Description = "Interview for the position of data analyst", Address = "Bergen Street 12, Bergen" },
            new Interview() { Id = 5, CompanyName = "Sopra Steria", Title = "ServiceNow Engineer Interview", Description = "Interview for the position of ServiceNow Engineer", Address = "Biskop Gunnerus Gate 14A, Oslo" },
            new Interview() { Id = 6, CompanyName = "Accenture", Title = "Cloud Engineer Interview", Description = "Interview to get to know each other", Address = "Rådhusgata 27, Oslo" },
            new Interview() { Id = 7, CompanyName = "Telenor", Title = "Software Developer Interview", Description = "Interview for the position of software developer", Address = "Snarøyveien 30, Fornebu" },
            new Interview() { Id = 8, CompanyName = "DNB", Title = "Data Scientist Interview", Description = " Case Interview for the position of data scientist", Address = "Dronning Eufemias gate 30, Oslo" }

        );

        modelBuilder.Entity<UserInterview>().HasData(
            new UserInterview() { Id = 1, UserId = 1, InterviewId = 1, isArchived = false, DurationInMinutes = 120, InterviewTime = new DateTime(2024, 11, 11, 14, 30, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 2, UserId = 1, InterviewId = 2, isArchived = false, DurationInMinutes = 90, InterviewTime = new DateTime(2024, 11, 15, 10, 0, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 3, UserId = 1, InterviewId = 3, isArchived = false, DurationInMinutes = 60, InterviewTime = new DateTime(2024, 11, 16, 11, 15, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 4, UserId = 1, InterviewId = 4, isArchived = false, DurationInMinutes = 45, InterviewTime = new DateTime(2024, 11, 18, 15, 45, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 5, UserId = 1, InterviewId = 5, isArchived = false, DurationInMinutes = 60, InterviewTime = new DateTime(2024, 11, 20, 13, 30, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 6, UserId = 2, InterviewId = 2, isArchived = false, DurationInMinutes = 90, InterviewTime = new DateTime(2024, 11, 15, 10, 0, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 7, UserId = 3, InterviewId = 3, isArchived = false, DurationInMinutes = 60, InterviewTime = new DateTime(2024, 11, 16, 11, 15, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 8, UserId = 4, InterviewId = 4, isArchived = false, DurationInMinutes = 45, InterviewTime = new DateTime(2024, 11, 18, 15, 45, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 9, UserId = 5, InterviewId = 5, isArchived = false, DurationInMinutes = 60, InterviewTime = new DateTime(2024, 11, 20, 13, 30, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 10, UserId = 6, InterviewId = 6, isArchived = false, DurationInMinutes = 90, InterviewTime = new DateTime(2024, 11, 22, 14, 0, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 11, UserId = 7, InterviewId = 7, isArchived = false, DurationInMinutes = 60, InterviewTime = new DateTime(2024, 11, 24, 10, 30, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 12, UserId = 8, InterviewId = 8, isArchived = false, DurationInMinutes = 90, InterviewTime = new DateTime(2024, 11, 26, 11, 0, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled }
        );

        modelBuilder.Entity<Logbook>().HasData(
            new Logbook() { Id = 1, UserId = 1, Logs = new List<Log>(), Title = "Ali's Logbook" },
            new Logbook() { Id = 2, UserId = 2, Logs = new List<Log>(), Title = "Muaath's Logbook" },
            new Logbook() { Id = 3, UserId = 3, Logs = new List<Log>(), Title = "John's Logbook" },
            new Logbook() { Id = 4, UserId = 4, Logs = new List<Log>(), Title = "Magnus's Logbook" },
            new Logbook() { Id = 5, UserId = 5, Logs = new List<Log>(), Title = "Sophia's Logbook" },
            new Logbook() { Id = 6, UserId = 6, Logs = new List<Log>(), Title = "David's Logbook" },
            new Logbook() { Id = 7, UserId = 7, Logs = new List<Log>(), Title = "Tina's Logbook" },
            new Logbook() { Id = 8, UserId = 8, Logs = new List<Log>(), Title = "Linda's Logbook" }
        );

        modelBuilder.Entity<Log>().HasData(
      new Log()
      {
          Id = 1,
          LogbookId = 1,
          Title = "Log 1",
          Content = "Learned about interview preparation and key technical questions",
          InterviewId = 1,
          Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Behavioral }
      },
      new Log()
      {
          Id = 2,
          LogbookId = 1,
          Title = "Log 2",
          Content = "Studied Python and algorithms for the next interview",
          InterviewId = 1,
          Label = new List<LogLabel> { LogLabel.Coding, LogLabel.Technical }
      },
      new Log()
      {
          Id = 3,
          LogbookId = 2,
          Title = "Log 1",
          Content = "Discovered effective ways to answer behavioral questions",
          InterviewId = 3,
          Label = new List<LogLabel> { LogLabel.Behavioral, LogLabel.SoftSkill }
      },
      new Log()
      {
          Id = 4,
          LogbookId = 2,
          Title = "Log 2",
          Content = "Reviewed data analysis tools like Excel, Tableau, and Power BI",
          InterviewId = 3,
          Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Design }
      },
      new Log()
      {
          Id = 5,
          LogbookId = 3,
          Title = "Log 1",
          Content = "Prepared for coding tests and problem-solving strategies",
          InterviewId = 4,
          Label = new List<LogLabel> { LogLabel.Coding, LogLabel.Technical }
      },
      new Log()
      {
          Id = 6,
          LogbookId = 3,
          Title = "Log 2",
          Content = "Analyzed data sets and created data reports",
          InterviewId = 4,
          Label = new List<LogLabel> { LogLabel.Documentation, LogLabel.Technical }
      },
      new Log()
      {
          Id = 7,
          LogbookId = 4,
          Title = "Log 1",
          Content = "Learned SQL database optimization techniques",
          InterviewId = 2,
          Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Coding }
      },
      new Log()
      {
          Id = 8,
          LogbookId = 4,
          Title = "Log 2",
          Content = "Focused on advanced SQL queries for interviews",
          InterviewId = 2,
          Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Coding }
      },

      new Log() { Id = 9, LogbookId = 5, Title = "Log 1", Content = "Reviewed STAR method for behavioral questions", InterviewId = 3, Label = new List<LogLabel> { LogLabel.Behavioral, LogLabel.SoftSkill }, },
      new Log() { Id = 10, LogbookId = 5, Title = "Log 2", Content = "Worked on data cleaning and data analysis", InterviewId = 4, Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Documentation }, },
      new Log() { Id = 11, LogbookId = 6, Title = "Log 1", Content = "Reviewed cloud computing concepts and cloud services", InterviewId = 6, Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Design }, },
      new Log() { Id = 12, LogbookId = 6, Title = "Log 2", Content = "Prepared for cloud engineer interview questions", InterviewId = 6, Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Coding }, },
      new Log() { Id = 13, LogbookId = 7, Title = "Log 1", Content = "Reviewed software development life cycle and agile methodologies", InterviewId = 7, Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Managerial }, },
      new Log() { Id = 14, LogbookId = 7, Title = "Log 2", Content = "Prepared for software developer interview questions", InterviewId = 7, Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Coding }, },
      new Log() { Id = 15, LogbookId = 8, Title = "Log 1", Content = "Reviewed data analysis tools and data visualization techniques", InterviewId = 8, Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Design }, },
      new Log() { Id = 16, LogbookId = 8, Title = "Log 2", Content = "Prepared for data scientist interview questions", InterviewId = 8, Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Coding }, },
      new Log() { Id = 17, LogbookId = 1, Title = "Log 1", Content = "Learned about interview preparation and key technical questions", InterviewId = 2, Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Behavioral }, },
      new Log() { Id = 18, LogbookId = 1, Title = "Log 2", Content = "Studied Python and algorithms for the next interview", InterviewId = 2, Label = new List<LogLabel> { LogLabel.Coding, LogLabel.Technical }, },
      new Log() { Id = 19, LogbookId = 1, Title = "Log 1", Content = "Discovered effective ways to answer behavioral questions", InterviewId = 3, Label = new List<LogLabel> { LogLabel.Behavioral, LogLabel.SoftSkill }, },
      new Log() { Id = 20, LogbookId = 1, Title = "Log 2", Content = "Reviewed data analysis tools like Excel, Tableau, and Power BI", InterviewId = 3, Label = new List<LogLabel> { LogLabel.Technical, LogLabel.Design }, },
      new Log() { Id = 21, LogbookId = 1, Title = "Log 1", Content = "Prepared for coding tests and problem-solving strategies", InterviewId = 4, Label = new List<LogLabel> { LogLabel.Coding, LogLabel.Technical }, },
      new Log() { Id = 22, LogbookId = 1, Title = "Log 2", Content = "Analyzed data sets and created data reports", InterviewId = 4, Label = new List<LogLabel> { LogLabel.Documentation, LogLabel.Technical }, }

  );

        modelBuilder.Entity<Note>().HasData(
            new Note() { Id = 1, Title = "Quick note from first interview", Content = "Need to smile more on interviews", UserInterviewId = 1, Status = NoteStatus.Reviewed },
            new Note() { Id = 2, Title = "Technical question review", Content = "Reviewed algorithms and problem-solving questions", UserInterviewId = 1, Status = NoteStatus.Reviewed },
            new Note() { Id = 3, Title = "Behavioral question notes", Content = "Need to work on STAR method for behavioral questions", UserInterviewId = 2, Status = NoteStatus.NotReviewed },
            new Note() { Id = 4, Title = "Data analysis feedback", Content = "Worked on cleaning data sets for the upcoming interview", UserInterviewId = 2, Status = NoteStatus.NotReviewed },
            new Note() { Id = 5, Title = "SQL skills review", Content = "Reviewed optimization techniques and SQL queries", UserInterviewId = 3, Status = NoteStatus.Reviewed },
            new Note() { Id = 6, Title = "Soft skills review", Content = "Need to smile more and show my soft skills", UserInterviewId = 3, Status = NoteStatus.NotReviewed },
            new Note() { Id = 7, Title = "Cloud computing concepts", Content = "Reviewed cloud computing concepts and cloud services", UserInterviewId = 4, Status = NoteStatus.Reviewed },
            new Note() { Id = 8, Title = "Cloud engineer interview prep", Content = "Prepared for cloud engineer interview questions", UserInterviewId = 4, Status = NoteStatus.Reviewed },
            new Note() { Id = 9, Title = "Software development life cycle", Content = "Reviewed software development life cycle and agile methodologies", UserInterviewId = 5, Status = NoteStatus.Reviewed },
            new Note() { Id = 10, Title = "Software developer interview prep", Content = "Prepared for software developer interview questions", UserInterviewId = 5, Status = NoteStatus.Reviewed },
            new Note() { Id = 11, Title = "Data analysis tools review", Content = "Reviewed data analysis tools and data visualization techniques", UserInterviewId = 6, Status = NoteStatus.Reviewed },
            new Note() { Id = 12, Title = "Data scientist interview prep", Content = "Prepared for data scientist interview questions", UserInterviewId = 6, Status = NoteStatus.Reviewed },
            new Note() { Id = 13, Title = "Quick note from first interview", Content = "Need to smile more on interviews", UserInterviewId = 7, Status = NoteStatus.Reviewed },
            new Note() { Id = 14, Title = "Technical question review", Content = "Reviewed algorithms and problem-solving questions", UserInterviewId = 7, Status = NoteStatus.Reviewed },
            new Note() { Id = 15, Title = "Behavioral question notes", Content = "Need to work on STAR method for behavioral questions", UserInterviewId = 8, Status = NoteStatus.NotReviewed },
            new Note() { Id = 16, Title = "Data analysis feedback", Content = "Worked on cleaning data sets for the upcoming interview", UserInterviewId = 8, Status = NoteStatus.NotReviewed }
        );
    }

    private List<User> SeedImageForUsers()
    {
        var userList = new List<User>();
        var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SeedImages");
        var defaultImagePath = Path.Combine(imageDirectory, "default1.jpg");

        if (!File.Exists(defaultImagePath))
        {
            throw new FileNotFoundException($"Default image not found at {defaultImagePath}");
        }
        var defaultImageBytes = File.ReadAllBytes(defaultImagePath);
        var defaultImageBase64 = Convert.ToBase64String(defaultImageBytes);
        var defaultImageDataUrl = $"data:image/jpeg;base64,{defaultImageBase64}";

        userList.Add(new User() { Id = 1, Username = "Ali Khan", PasswordHash = SeedData.HashPassword("ali123"), Email = "ali@example.com", Mobile = "1234", LogbookId = 1, ProfileImage = defaultImageDataUrl });
        userList.Add(new User() { Id = 2, Username = "Muaath Zerouga", PasswordHash = SeedData.HashPassword("muaath123"), Email = "muaath@example.com", Mobile = "1881", LogbookId = 2, ProfileImage = defaultImageDataUrl });
        userList.Add(new User() { Id = 3, Username = "John Ferdie", PasswordHash = SeedData.HashPassword("john123"), Email = "john@example.com", Mobile = "123", LogbookId = 3, ProfileImage = defaultImageDataUrl });
        userList.Add(new User() { Id = 4, Username = "Magnus Brandsegg", PasswordHash = SeedData.HashPassword("magnus123"), Email = "magnus@example.com", Mobile = "786", LogbookId = 4, ProfileImage = defaultImageDataUrl });
        userList.Add(new User() { Id = 5, Username = "Sophia Miller", PasswordHash = SeedData.HashPassword("sophia123"), Email = "sophia@example.com", Mobile = "2250", LogbookId = 5, ProfileImage = defaultImageDataUrl });
        userList.Add(new User() { Id = 6, Username = "David Johnson", PasswordHash = SeedData.HashPassword("david123"), Email = "david@example.com", Mobile = "4332", LogbookId = 6, ProfileImage = defaultImageDataUrl });
        userList.Add(new User() { Id = 7, Username = "Tina Miller", PasswordHash = SeedData.HashPassword("tina123"), Email = "tina@example.com", Mobile = "9999", LogbookId = 7, ProfileImage = defaultImageDataUrl });
        userList.Add(new User() { Id = 8, Username = "Linda Johnson", PasswordHash = SeedData.HashPassword("linda786"), Email = "linda@example.com", Mobile = "1884", LogbookId = 8, ProfileImage = defaultImageDataUrl });

            
        return userList;
    }

        public DbSet<User> Users { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<UserInterview> UserInterviews { get; set; }
        public DbSet<Logbook> Logbooks { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Log> Logs { get; set; }


    
}