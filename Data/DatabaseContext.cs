
using Interview_Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



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

        modelBuilder.Entity<Logbook>()
            .HasOne(p => p.User)
            .WithOne(p => p.Logbook)
            .HasForeignKey<Logbook>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>().HasData(

            new User() { UserId = 1, Username = "Ali Khan", PasswordHash = SeedData.HashPassword("ali123"), Email = "ali@example.com", Mobile = "1234", LogbookId = 1 },
            new User() { UserId = 2, Username = "Muaath Zerouga", PasswordHash = SeedData.HashPassword("muaath123"), Email = "muaath@example.com", Mobile = "1881", LogbookId = 2 },
            new User() { UserId = 3, Username = "John Ferdie", PasswordHash = SeedData.HashPassword("john123"), Email = "john@example.com", Mobile = "123", LogbookId = 3 },
            new User() { UserId = 4, Username = "Magnus Brandsegg", PasswordHash = SeedData.HashPassword("magnus123"), Email = "magnus@example.com", Mobile = "786", LogbookId = 4 });
            
                

    }

        public DbSet<User> Users { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<UserInterview> UserInterviews { get; set; }
        public DbSet<Logbook> Logbooks { get; set; }
        public DbSet<Note> Notes { get; set; }


    
}