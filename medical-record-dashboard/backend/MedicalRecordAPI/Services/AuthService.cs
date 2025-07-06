using Microsoft.EntityFrameworkCore;
using MedicalRecordAPI.Data;
using MedicalRecordAPI.DTOs;
using MedicalRecordAPI.Models;

namespace MedicalRecordAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (existingUser != null)
                return null;

            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Gender = registerDto.Gender,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User?> UpdateUserAsync(int userId, UpdateUserDto updateDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return null;

            if (!string.IsNullOrEmpty(updateDto.Email))
                user.Email = updateDto.Email;
            
            if (!string.IsNullOrEmpty(updateDto.Gender))
                user.Gender = updateDto.Gender;
            
            if (!string.IsNullOrEmpty(updateDto.PhoneNumber))
                user.PhoneNumber = updateDto.PhoneNumber;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateProfileImageAsync(int userId, string imagePath)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.ProfileImagePath = imagePath;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}