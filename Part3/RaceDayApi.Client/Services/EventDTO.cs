namespace RaceDayApi.Client.Services
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string OrganizerName { get; set; } = string.Empty;
        public string OrganizerEmail { get; set; } = string.Empty;
        public string OrganizerId { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
    }
}