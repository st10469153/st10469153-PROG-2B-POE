namespace RaceDayApplicationApi.Models.DTOs
{
    public class RegisterDto
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Role { get; set; }
    }
}
