namespace RaceDayApplicationApi.Models.DTOs
{
    public class UpdateEventDto
    {
        public string EventName { get; set; } = string.Empty;
        public string OrganizerName { get; set; } = string.Empty;
        public ICollection<Event> Enrolments { get; set; } = new List<Event>();
    }
}
