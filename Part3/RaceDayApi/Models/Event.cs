using System.ComponentModel.DataAnnotations;
using RaceDayApi.Models;
using System.Collections.Generic;

namespace RaceDayApi.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public required string LectureName { get; set; }
        public ICollection<EventEnrollments> EventEnrollments { get; set; } = new List<EventEnrollments>();
    }
}

