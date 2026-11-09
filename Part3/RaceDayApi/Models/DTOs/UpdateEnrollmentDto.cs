namespace RaceDayApi.Models.DTOs
{
    public class UpdateEnrollmentDto
    {
        public int ParicipantId { get; set; }
        public int EventId { get; set; }

        public DateOnly EnrolmentDate { get; set; } = new DateOnly();

    }
}
