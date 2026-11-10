using Microsoft.AspNetCore.Mvc;

namespace RaceDayApi.Client.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
