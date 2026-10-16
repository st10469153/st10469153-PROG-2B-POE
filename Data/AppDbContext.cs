using RaceDayApplicationApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RaceDayApplicationApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public DbSet<UserAccount> Users { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        //public DbSet<EnrolmentStatus> EnrolmentStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Pass 2 here
            // ---------- Organizer ----------
            modelBuilder.Entity<Organizer>(e =>
            {
                e.ToTable("Organizer");
                e.Property(x => x.Name).HasMaxLength(50).IsRequired();
                e.Property(x => x.Surname).HasMaxLength(50).IsRequired();
                e.Property(x => x.Email).HasMaxLength(100).IsRequired();
                e.Property(x => x.PasswordHash).HasMaxLength(255).IsRequired();
                e.HasIndex(x => x.Email).IsUnique();
            });

            // ---------- PARTICIPANT ----------
                modelBuilder.Entity<Participant>()
                .ToTable("PARTICIPANT")
                .Property(x => x.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Participant>()
                .Property(x => x.Surname).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Participant>()
                .Property(x => x.DateOfBirth).HasColumnType("date");

            modelBuilder.Entity<Participant>()
                .Property(x => x.Email).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Participant>()
                .Property(x => x.PasswordHash).HasMaxLength(255).IsRequired();

            modelBuilder.Entity<Participant>()
                .Property(x => x.ProfilePictureUrl).HasMaxLength(500);

            modelBuilder.Entity<Participant>()
                .HasIndex(x => x.Email).IsUnique();

            // ---------- EVENT ----------
            modelBuilder.Entity<Event>(e =>
            {
                e.ToTable("EVENT", t =>
                {
                    t.HasCheckConstraint("CK_EVENT_EventType", "[EventType] IN ('Run', 'Walk', 'Cycle')");
                    t.HasCheckConstraint("CK_EVENT_Distance", "[Distance] > 0");
                });
                e.Property(x => x.EventName).HasMaxLength(100).IsRequired();
                e.Property(x => x.EventType).HasConversion<string>().HasMaxLength(20);
                e.Property(x => x.Description).IsRequired();               // nvarchar(max)
                e.Property(x => x.Distance).HasPrecision(5, 2);
                e.Property(x => x.Location).HasMaxLength(150).IsRequired();
                e.Property(x => x.Date).HasColumnType("datetime");
                e.Property(x => x.BannerImageUrl).HasMaxLength(500);

                e.HasOne(x => x.Organizer)
                 .WithMany(o => o.Events)
                 .HasForeignKey(x => x.OrganizerId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // ---------- CATEGORY ----------
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("CATEGORY");
                e.Property(x => x.CategoryName).HasMaxLength(50).IsRequired();
                e.Property(x => x.Description).HasMaxLength(255);
                e.HasIndex(x => new { x.EventId, x.CategoryName }).IsUnique();

                e.HasOne(x => x.Event)
                 .WithMany(ev => ev.Categories)
                 .HasForeignKey(x => x.EventId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // ---------- ENROLMENT ----------
            modelBuilder.Entity<Enrolment>(e =>
            {
                e.ToTable("ENROLMENT", t =>
                    t.HasCheckConstraint("CK_ENROLMENT_Status",
                        "[EnrolmentStatus] IN ('Pending', 'Confirmed', 'Cancelled')"));

                e.Property(x => x.EnrolmentDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
                e.Property(x => x.EnrolmentStatus)
                 .HasConversion<string>()
                 .HasMaxLength(20)
                 .HasDefaultValue(EnrolmentStatus.Pending);

                e.HasIndex(x => new { x.ParticipantId, x.EventId }).IsUnique();

                e.HasOne(x => x.Participant).WithMany(p => p.Enrolments)
                 .HasForeignKey(x => x.ParticipantId).OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Event).WithMany(ev => ev.Enrolments)
                 .HasForeignKey(x => x.EventId).OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Category).WithMany(c => c.Enrolments)
                 .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            });

            // ---------- RESULT ----------
            modelBuilder.Entity<Result>(e =>
            {
                e.ToTable("RESULT", t =>
                    t.HasCheckConstraint("CK_RESULT_Position", "[FinishingPosition] > 0"));

                e.Property(x => x.FinishingTime).HasColumnType("time(0)");
                e.Property(x => x.IsPublished).HasDefaultValue(false);
                e.HasIndex(x => x.EnrolmentId).IsUnique();

                e.HasOne(x => x.Enrolment)
                 .WithOne(en => en.Result)
                 .HasForeignKey<Result>(x => x.EnrolmentId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
