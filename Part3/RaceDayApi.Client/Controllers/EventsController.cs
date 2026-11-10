using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Client.Services;
using Microsoft.AspNetCore.Http;


namespace RaceDayApi.Client.Controllers
{
    public class EventsController : Controller
    {
        private readonly IHttpClientFactory _factory;
        public EventsController(IHttpClientFactory factory)
        {
                       _factory = factory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _factory.CreateClient("RaceDayApi");
            var response = await client.GetAsync("api/events");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Cannot load courses.";
                return View(new List<EventDTO>());
            }
            var events = await response.Content.ReadFromJsonAsync<List<EventDTO>>();
            return View(events);
        }

        [HttpGet]
        public IActionResult Create()
        {
            string? role = HttpContext.Session.GetString("Role");
            if (role != "Organizer")
                return RedirectToAction("AccessDenied", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult Create(EventDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);
            // Save the event (this would typically involve calling a service)
            return RedirectToAction(nameof(Index));
        }
    }
}


//namespace RaceDayApi.Client.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EventsController : ControllerBase
//    {
//        private readonly EventApiService _events;
//        public EventsController(EventApiService events)
//        {
//            _events = events;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            var events = await _events.GetEventsAsync();
//            return Ok(events);
//        }

//        [HttpGet("create")]
//        public IActionResult Create()
//        {
//            string? role = HttpContext.Session.GetString("Role");
//            if (role != "Organizer")
//                return RedirectToAction("AccessDenied", "Account");
//            return Ok();
//        }

//        [HttpPost("create")]
//        public async Task<IActionResult> Create(EventDTO model)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);
//            bool saved = await _events.CreateEventAsync(model);
//            if (!saved)
//            {
//                ModelState.AddModelError("", "Could not save the event.");
//                return BadRequest(ModelState);
//            }
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
