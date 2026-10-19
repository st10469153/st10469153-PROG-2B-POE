namespace RaceDayApplicationApi.Models.DTOs
{
    public class ProfileDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public DateTime dateOfBirth { get; set; } = DateTime.Now;
        public string PfpUrl { get; set; } = string.Empty;
    }
}
