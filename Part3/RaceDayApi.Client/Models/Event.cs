namespace RaceDayApi.Client.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = "";
        public string OrganizerName { get; set; } = "";
        public string EventDate { get; set; } = "";
        public string EventTime { get; set; } = "";
        public string EventLocation { get; set; } = "";
        public string EventStatus { get; set; } = "";
        public string EventType { get; set; } = "";
    }
}
