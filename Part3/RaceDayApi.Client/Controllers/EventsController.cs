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
    }
}
