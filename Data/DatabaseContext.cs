
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

        modelBuilder.Entity<Note>()
            .Property(n => n.Status)
            .HasConversion<string>();

        modelBuilder.Entity<UserInterview>()
            .Property(n => n.Status)
            .HasConversion<string>();

        modelBuilder.Entity<UserInterview>()
            .Property(n => n.Role)
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
        foreach ( var user in usersWithImages )
        {
            modelBuilder.Entity<User>().HasData(user);
        }

        

        modelBuilder.Entity<Interview>().HasData(
            new Interview() { Id = 1, CompanyName = "PayEx", Title = "Technical Interview", Description = "Technical interview after a short speedinterview", Address = "Kongens gate 6, Oslo" },
            new Interview() { Id = 2, CompanyName = "Nordre Follo Kommune", Title = "Førstegangsintervju", Description = "Bli kjent intervju", Address = "Idrettsveien 8, Ski" },
            new Interview() { Id = 3, CompanyName = "TechCorp Solutions", Title = "Software Engineer Interview", Description = "A technical interview for a software engineer position", Address = "Helsingborgveien 5, Bergen" },
            new Interview() { Id = 4, CompanyName = "GlobalTech Innovations", Title = "Data Analyst Interview", Description = "Interview for the position of data analyst", Address = "Bergen Street 12, Bergen" }
        );

        modelBuilder.Entity<UserInterview>().HasData(
            new UserInterview() { Id = 1, UserId = 1, InterviewId = 1, DurationInMinutes = 120, Role = UserRole.Interviewee, InterviewTime = new DateTime(2024, 11, 11, 14, 30, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 2, UserId = 2, InterviewId = 3, DurationInMinutes = 90, Role = UserRole.Interviewee, InterviewTime = new DateTime(2024, 11, 15, 10, 0, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 3, UserId = 3, InterviewId = 4, DurationInMinutes = 60, Role = UserRole.Interviewee, InterviewTime = new DateTime(2024, 11, 16, 11, 15, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled },
            new UserInterview() { Id = 4, UserId = 4, InterviewId = 2, DurationInMinutes = 45, Role = UserRole.Interviewee, InterviewTime = new DateTime(2024, 11, 18, 15, 45, 0, DateTimeKind.Utc), Status = InterviewStatus.Scheduled }
        );

        modelBuilder.Entity<Logbook>().HasData(
            new Logbook() { Id = 1, UserId = 1, Logs = new List<Log>(), Title = "Ali's Logbook" },
            new Logbook() { Id = 2, UserId = 2, Logs = new List<Log>(), Title = "Muaath's Logbook" },
            new Logbook() { Id = 3, UserId = 3, Logs = new List<Log>(), Title = "John's Logbook" },
            new Logbook() { Id = 4, UserId = 4, Logs = new List<Log>(), Title = "Magnus's Logbook" },
            new Logbook() { Id = 5, UserId = 5, Logs = new List<Log>(), Title = "Sophia's Logbook" },
            new Logbook() { Id = 6, UserId = 6, Logs = new List<Log>(), Title = "David's Logbook" }
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
      }
  );

        modelBuilder.Entity<Note>().HasData(
            new Note() { Id = 1, Title = "Quick note from first interview", Content = "Need to smile more on interviews", UserInterviewId = 1, Status = NoteStatus.Reviewed },
            new Note() { Id = 2, Title = "Technical question review", Content = "Reviewed algorithms and problem-solving questions", UserInterviewId = 2, Status = NoteStatus.Reviewed },
            new Note() { Id = 3, Title = "Behavioral question notes", Content = "Need to work on STAR method for behavioral questions", UserInterviewId = 3, Status = NoteStatus.NotReviewed },
            new Note() { Id = 4, Title = "Data analysis feedback", Content = "Worked on cleaning data sets for the upcoming interview", UserInterviewId = 4, Status = NoteStatus.NotReviewed },
            new Note() { Id = 5, Title = "SQL skills review", Content = "Reviewed optimization techniques and SQL queries", UserInterviewId = 4, Status = NoteStatus.Reviewed }
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
        userList.Add(new User() { Id = 1, Username = "Ali Khan", PasswordHash = SeedData.HashPassword("ali123"), Email = "ali@example.com", Mobile = "1234", LogbookId = 1, ProfileImage = defaultImageBytes });
        userList.Add(new User() { Id = 2, Username = "Muaath Zerouga", PasswordHash = SeedData.HashPassword("muaath123"), Email = "muaath@example.com", Mobile = "1881", LogbookId = 2, ProfileImage = defaultImageBytes });
        userList.Add(new User() { Id = 3, Username = "John Ferdie", PasswordHash = SeedData.HashPassword("john123"), Email = "john@example.com", Mobile = "123", LogbookId = 3,ProfileImage = defaultImageBytes });
        userList.Add(new User() { Id = 4, Username = "Magnus Brandsegg", PasswordHash = SeedData.HashPassword("magnus123"), Email = "magnus@example.com", Mobile = "786", LogbookId = 4 , ProfileImage = defaultImageBytes });
        userList.Add(new User() { Id = 5, Username = "Sophia Miller", PasswordHash = SeedData.HashPassword("sophia123"), Email = "sophia@example.com", Mobile = "2250", LogbookId = 5, ProfileImage = defaultImageBytes });
        userList.Add(new User() { Id = 6, Username = "David Johnson", PasswordHash = SeedData.HashPassword("david123"), Email = "david@example.com", Mobile = "4332", LogbookId = 6 , ProfileImage = defaultImageBytes });
        return userList;
       }

    public DbSet<User> Users { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<UserInterview> UserInterviews { get; set; }
        public DbSet<Logbook> Logbooks { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Log> Logs { get; set; }


    
}