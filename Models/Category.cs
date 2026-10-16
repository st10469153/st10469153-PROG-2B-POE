using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDayApplicationApi.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }                 // CategoryID INT IDENTITY(1,1) PRIMARY KEY

        [Required]
        public int EventId { get; set; }                    // EventID INT NOT NULL (FK)

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }            // NVARCHAR(50) NOT NULL

        [MaxLength(255)]
        public string? Description { get; set; }            // NVARCHAR(255) NULL

        // Navigation
        public Event? Event { get; set; }
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    }
}