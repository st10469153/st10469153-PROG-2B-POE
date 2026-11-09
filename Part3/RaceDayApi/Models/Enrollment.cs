using RaceDayApi.Models;
using System.ComponentModel.DataAnnotations;

namespace RaceDayApi.Models
{
    public class EventEnrollments
    {
        [Key]
        public int EventEnrollmentId { get; set; }
        public int StudentId { get; set; }
        public required Participant Participant { get; set; }
        public int EventId { get; set; }
        public required Event Event { get; set; }
        public DateOnly RegistrationDate { get; set; }
    }
}
