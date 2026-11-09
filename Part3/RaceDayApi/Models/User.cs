using Microsoft.EntityFrameworkCore;

namespace RaceDayApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // e.g., "Participant OR Organizer"
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventEnrollments> EventEnrollments { get; set; }
    }

    public class UserLoginDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}



// User Account from

namespace RaceDayApi.Models
{
    public class UserAccount
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Participant"; // Default role is Participant
    }
}
