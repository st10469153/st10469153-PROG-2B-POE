namespace RaceDayApplicationApi.Models
{
    public class Result
    {
        public int ResultId { get; set; }                   // ResultID INT IDENTITY(1,1) PRIMARY KEY

        public int EnrolmentId { get; set; }                // EnrolmentID INT NOT NULL UNIQUE (FK)

        public TimeSpan FinishingTime { get; set; }         // TIME(0) NOT NULL

        public int FinishingPosition { get; set; }          // INT NOT NULL CHECK (FinishingPosition > 0)

        public bool IsPublished { get; set; }               // BIT NOT NULL DEFAULT 0

        // Navigation
        public Enrolment? Enrolment { get; set; }
    }
}
