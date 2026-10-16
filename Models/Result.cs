using System;
using System.ComponentModel.DataAnnotations;

namespace RaceDayApplicationApi.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }                   // ResultID INT IDENTITY(1,1) PRIMARY KEY

        [Required]
        public int EnrolmentId { get; set; }                // EnrolmentID INT NOT NULL UNIQUE (FK)

        [Required]
        public TimeSpan FinishingTime { get; set; }         // TIME(0) NOT NULL

        [Required]
        public int FinishingPosition { get; set; }          // INT NOT NULL CHECK (FinishingPosition > 0)

        [Required]
        public bool IsPublished { get; set; }               // BIT NOT NULL DEFAULT 0

        // Navigation
        public Enrolment? Enrolment { get; set; }
    }
}