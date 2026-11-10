using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Client.Models;
using RaceDayApi.Client.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace RaceDayApi.Client.Controllers
{
    public class AccountController : Controller
    {
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

        private readonly IHttpClientFactory _factory;
        public AccountController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public IActionResult Login()
            => View(new LoginViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var client = _factory.CreateClient("RaceDayApi");
            var response = new
            {
                email = model.Email,
                hashPassword = model.Password
            };
            var response = await client.PostAsJsonAsync("api/Auth/login", response);
            if(!response.IsSuccessStatusCode) return View(model);
            var login = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (login == null || string.IsNullOrWhiteSpace(login.Token)) return View(model);
            HttpContext.Session.SetString("Token", login.Token);
            HttpContext.Session.SetString("Role", login.Role);
            return RedirectToAction("Index", "Dashboard");

        }

        var token = HttpContext.Session.GetString("Token");
        if(string.IsNullOrWhiteSpace(token))
            return RedirectToActionResult("Login", "Account");

        var role = HttpContext.Session.GetString("Role");
        if(role == "Participant") return View("Participant");
        if (role == "Organizer") return View("Organizer");
        return Forbid();
    }
