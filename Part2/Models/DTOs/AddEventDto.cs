namespace RaceDayApplicationApi.Models.DTOs
{
    public class AddCEventDto
    {
        public string EventName = string.Empty;
        public string OrganizerName = string.Empty;
        public ICollection<Event> Enrolments { get; set; } = new List<Event>();
    }
}
