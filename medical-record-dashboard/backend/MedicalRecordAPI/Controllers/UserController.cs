using Microsoft.AspNetCore.Mvc;
using MedicalRecordAPI.DTOs;
using MedicalRecordAPI.Services;

namespace MedicalRecordAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public UserController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto updateDto)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
                return Unauthorized(new { message = "Not authenticated" });

            var user = await _authService.UpdateUserAsync(userId.Value, updateDto);
            
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

        [HttpPost("profile-image")]
        public async Task<IActionResult> UploadProfileImage([FromForm] IFormFile image)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
                return Unauthorized(new { message = "Not authenticated" });

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var fileExtension = Path.GetExtension(image.FileName).ToLower();
            
            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest(new { message = "Invalid file type. Only image files are allowed." });

            if (image.Length > 5 * 1024 * 1024)
                return BadRequest(new { message = "File size must be less than 5MB" });

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-images");
            
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var uniqueFileName = $"{userId}_{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var result = await _authService.UpdateProfileImageAsync(userId.Value, $"/profile-images/{uniqueFileName}");
            
            if (!result)
                return NotFound(new { message = "User not found" });

            return Ok(new { profileImagePath = $"/profile-images/{uniqueFileName}" });
        }
    }
}