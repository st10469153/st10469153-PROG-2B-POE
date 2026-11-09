using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RaceDayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        // Accessible by anyone (Public)
        [HttpGet]
        public IActionResult GetPublicEvents()
        {
            return Ok(new[] { "Laptop", "Mouse", "Keyboard" });
        }

        // Accessible only by authenticated users (regardless of role)
        [HttpGet("secured")]
        [Authorize]
        public IActionResult GetUserEvents()
        {
            var username = User.Identity?.Name;
            return Ok(new { Message = $"Hello {username}, here are your restricted products.", Items = new[] { "Server Access", "Cloud Storage" } });
        }

        // Accessible ONLY by users with the "Admin" Role
        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public IActionResult CreateProduct([FromBody] string productName)
        {
            return Ok(new { Message = $"Product '{productName}' successfully created by Admin." });
        }
    }
}