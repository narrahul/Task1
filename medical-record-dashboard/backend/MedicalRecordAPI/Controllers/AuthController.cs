using Microsoft.AspNetCore.Mvc;
using MedicalRecordAPI.DTOs;
using MedicalRecordAPI.Services;

namespace MedicalRecordAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = await _authService.RegisterAsync(registerDto);
            
            if (user == null)
                return BadRequest(new { message = "Email already exists" });

            HttpContext.Session.SetInt32("UserId", user.Id);
            
            return Ok(new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                ProfileImagePath = user.ProfileImagePath
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _authService.LoginAsync(loginDto);
            
            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            HttpContext.Session.SetInt32("UserId", user.Id);
            
            return Ok(new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                ProfileImagePath = user.ProfileImagePath
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
                return Unauthorized(new { message = "Not authenticated" });

            var user = await _authService.GetUserByIdAsync(userId.Value);
            
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                ProfileImagePath = user.ProfileImagePath
            });
        }
    }
}