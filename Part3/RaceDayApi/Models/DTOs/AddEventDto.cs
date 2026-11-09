using RaceDayApi.Models;

namespace RaceDayApi.Models.DTOs
{
    public class AddEventDto
    {
        public string EventName = string.Empty;
        public string OrganizerName = string.Empty;
        public ICollection<Event> Enrolments { get; set; } = new List<Event>();
    }
}



