using MedicalRecordAPI.DTOs;
using MedicalRecordAPI.Models;

namespace MedicalRecordAPI.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterDto registerDto);
        Task<User?> LoginAsync(LoginDto loginDto);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> UpdateUserAsync(int userId, UpdateUserDto updateDto);
        Task<bool> UpdateProfileImageAsync(int userId, string imagePath);
    }
}