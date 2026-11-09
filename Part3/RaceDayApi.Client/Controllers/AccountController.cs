using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Client.Models;
using RaceDayApi.Client.Services;
using Microsoft.AspNetCore.Http;

namespace RaceDayApi.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _auth;

        public AccountController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _auth.LoginAsync(model);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("Role", user.Role);
            return RedirectToAction("Index", "Home");
        }
    }
}
