using RaceDayApplicationApi.Data;
using RaceDayApplicationApi.Models;
using RaceDayApplicationApi.Models.DTOs;
using RaceDayApplicationApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return Ok("You are authenticated");
        }
        private readonly AppDbContext _db;
        private readonly IPasswordHasher<UserAccount> _passwordhasher;
        private readonly JwtService _jwtService;
        public AuthController(
        AppDbContext db,
        IPasswordHasher<UserAccount> hasher,
        JwtService jwtService)
        {
            _db = db;
            _passwordhasher = hasher;
            _jwtService = jwtService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Email already exists.");
            }
            var user = new UserAccount
            {
                Email = dto.Email,
                Role = dto.Role
            };
            user.PasswordHash = _passwordhasher.HashPassword(user, dto.HashPassword);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok("User registered successfully.");

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user is null)
                return Unauthorized("Invalid login");
            var result = _passwordhasher.VerifyHashedPassword(
            user, user.PasswordHash, dto.HashPassword);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid login");
            var token = _jwtService.CreateToken(user);
            return Ok(new { token, role = user.Role });
        }


    }
}
    