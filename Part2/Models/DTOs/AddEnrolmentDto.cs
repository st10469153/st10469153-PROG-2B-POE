namespace CourseRegistrationApi.Models.DTOs
{
    public class AddEnrolmentDto
    {
        public int ParicipantId { get; set; }

        public int EventId { get; set; }

        public DateOnly EnrolmentDate { get; set; }
    }
}
