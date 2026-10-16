using System;
using System.ComponentModel.DataAnnotations;

namespace RaceDayApplicationApi.Models
{
    public class Enrolment
    {
        [Key]
        public int EnrolmentId { get; set; }                // EnrolmentID INT IDENTITY(1,1) PRIMARY KEY

        [Required]
        public int ParticipantId { get; set; }              // ParticipantID INT NOT NULL (FK)

        [Required]
        public int EventId { get; set; }                    // EventID INT NOT NULL (FK)

        [Required]
        public int CategoryId { get; set; }                 // CategoryID INT NOT NULL (FK)

        [Required]
        public DateTime EnrolmentDate { get; set; }         // DATETIME NOT NULL DEFAULT GETDATE()

        [Required]
        public EnrolmentStatus EnrolmentStatus { get; set; } // NVARCHAR(20) NOT NULL DEFAULT 'Pending' -> Enum

        // Navigation
        public Participant? Participant { get; set; }
        public Event? Event { get; set; }
        public Category? Category { get; set; }
        public Result? Result { get; set; }
    }
}