using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDayApplicationApi.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }              // ParticipantID INT IDENTITY(1,1) PRIMARY KEY

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }                    // NVARCHAR(50) NOT NULL

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }                 // NVARCHAR(50) NOT NULL

        [Required]
        public DateTime DateOfBirth { get; set; }           // DATE NOT NULL

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }                   // NVARCHAR(100) NOT NULL UNIQUE

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }            // NVARCHAR(255) NOT NULL

        [MaxLength(500)]
        public string? ProfilePictureUrl { get; set; }      // NVARCHAR(500) NULL

        // Navigation
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    }
}