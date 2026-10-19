namespace RaceDayApplicationApi.Models.DTOs
{
    public class UpdateEnrolmentDto
    {
        public int ParicipantId { get; set; }
        public int EventId { get; set; }

        public DateOnly EnrolmentDate { get; set; } = new DateOnly();
    }
}
