using System.ComponentModel.DataAnnotations;

namespace RaceDayApplicationApi.Models
{
    public class Organizer
    {
        [Key]
        public int OrganizerId { get; set; }                // OrganizerID INT IDENTITY(1,1) PRIMARY KEY

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;    // NVARCHAR(50) NOT NULL

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; } = string.Empty; // NVARCHAR(50) NOT NULL

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;   // NVARCHAR(100) NOT NULL UNIQUE

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty; // NVARCHAR(255) NOT NULL

        // Navigation
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
