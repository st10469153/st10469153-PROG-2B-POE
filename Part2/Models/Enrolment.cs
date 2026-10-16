using System.ComponentModel.DataAnnotations;

namespace RaceDayApplicationApi.Models
{
    public class Enrolment
    {
        [Key]
        public int EnrolmentId { get; set; }                // EnrolmentID INT IDENTITY(1,1) PRIMARY KEY

        public int ParticipantId { get; set; }              // ParticipantID INT NOT NULL (FK)
        public int EventId { get; set; }                    // EventID INT NOT NULL (FK)
        public int CategoryId { get; set; }                 // CategoryID INT NOT NULL (FK)

        public DateTime EnrolmentDate { get; set; }         // DATETIME NOT NULL DEFAULT GETDATE()

        public EnrolmentStatus EnrolmentStatus { get; set; } // NVARCHAR(20) NOT NULL DEFAULT 'Pending'

        // Navigation
        public Participant? Participant { get; set; }
        public Event? Event { get; set; }
        public Category? Category { get; set; }
        public Result? Result { get; set; }
    }
}
