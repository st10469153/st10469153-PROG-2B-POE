using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDayApplicationApi.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }                    // EventID INT IDENTITY(1,1) PRIMARY KEY

        [Required]
        public int OrganizerId { get; set; }                // OrganiserID INT NOT NULL (FK)

        [Required]
        [MaxLength(100)]
        public string EventName { get; set; }               // NVARCHAR(100) NOT NULL

        [Required]
        public EventType EventType { get; set; }            // NVARCHAR(20) NOT NULL CHECK (Run,Walk,Cycle) -> Enum

        [Required]
        public string Description { get; set; }             // NVARCHAR(MAX) NOT NULL

        [Required]
        public decimal Distance { get; set; }               // DECIMAL(5,2) NOT NULL CHECK (Distance > 0)

        [Required]
        [MaxLength(150)]
        public string Location { get; set; }                // NVARCHAR(150) NOT NULL

        [Required]
        public DateTime Date { get; set; }                  // DATETIME NOT NULL

        [MaxLength(500)]
        public string? BannerImageUrl { get; set; }         // NVARCHAR(500) NULL

        // Navigation
        public Organizer? Organizer { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    }
}