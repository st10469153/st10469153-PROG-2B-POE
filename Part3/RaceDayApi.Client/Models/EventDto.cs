namespace RaceDayApi.Client.Models
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string OrganizerName { get; set; } = string.Empty;
        public string? ImageBlobName { get; set; }
    }
}
