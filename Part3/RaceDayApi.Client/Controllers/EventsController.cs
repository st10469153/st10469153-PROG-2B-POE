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

        // Removed View() usages, replaced with appropriate IActionResult responses for API controller
        [HttpGet("create")]
        public IActionResult Create()
        {
            // For API controllers, typically return a template or default model, or just Ok()
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
