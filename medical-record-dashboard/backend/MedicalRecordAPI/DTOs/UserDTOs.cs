using System.ComponentModel.DataAnnotations;

namespace MedicalRecordAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression("^(male|female)$", ErrorMessage = "Gender must be either 'male' or 'female'")]
        public string Gender { get; set; } = string.Empty;
        
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class UpdateUserDto
    {
        [EmailAddress]
        public string? Email { get; set; }
        
        [RegularExpression("^(male|female)$", ErrorMessage = "Gender must be either 'male' or 'female'")]
        public string? Gender { get; set; }
        
        [Phone]
        public string? PhoneNumber { get; set; }
    }

    public class UserResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? ProfileImagePath { get; set; }
    }
}