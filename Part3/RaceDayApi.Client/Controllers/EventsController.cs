using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Client.Services;

namespace RaceDayApi.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventApiService _events;
        public EventsController(EventApiService events)
        {
            _events = events;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var events = await _events.GetEventsAsync();
            return Ok(events);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            string? role = HttpContext.Session.GetString("Role");
            if (role != "Organizer")
                return RedirectToAction("AccessDenied", "Account");
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(EventDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool saved = await _events.CreateEventAsync(model);
            if (!saved)
            {
                ModelState.AddModelError("", "Could not save the event.");
                return BadRequest(ModelState);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
