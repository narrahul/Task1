using Microsoft.EntityFrameworkCore;
using MedicalRecordAPI.Data;
using MedicalRecordAPI.DTOs;
using MedicalRecordAPI.Models;

namespace MedicalRecordAPI.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _uploadPath;

        public FileService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FileUpload:UploadPath"]!);
            
            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);
        }

        public async Task<MedicalFile?> UploadFileAsync(int userId, FileUploadDto fileDto)
        {
            var allowedExtensions = _configuration.GetSection("FileUpload:AllowedExtensions").Get<string[]>();
            var maxFileSize = _configuration.GetValue<int>("FileUpload:MaxFileSizeInMB") * 1024 * 1024;

            var fileExtension = Path.GetExtension(fileDto.File.FileName).ToLower();
            if (!allowedExtensions!.Contains(fileExtension))
                return null;

            if (fileDto.File.Length > maxFileSize)
                return null;

            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_uploadPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileDto.File.CopyToAsync(stream);
            }

            var medicalFile = new MedicalFile
            {
                UserId = userId,
                FileType = fileDto.FileType,
                FileName = fileDto.FileName,
                FilePath = uniqueFileName,
                ContentType = fileDto.File.ContentType,
                FileSize = fileDto.File.Length
            };

            _context.MedicalFiles.Add(medicalFile);
            await _context.SaveChangesAsync();

            return medicalFile;
        }

        public async Task<IEnumerable<MedicalFile>> GetUserFilesAsync(int userId)
        {
            return await _context.MedicalFiles
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.UploadedAt)
                .ToListAsync();
        }

        public async Task<MedicalFile?> GetFileByIdAsync(int fileId, int userId)
        {
            return await _context.MedicalFiles
                .FirstOrDefaultAsync(f => f.Id == fileId && f.UserId == userId);
        }

        public async Task<bool> DeleteFileAsync(int fileId, int userId)
        {
            var file = await GetFileByIdAsync(fileId, userId);
            if (file == null)
                return false;

            var filePath = Path.Combine(_uploadPath, file.FilePath);
            if (File.Exists(filePath))
                File.Delete(filePath);

            _context.MedicalFiles.Remove(file);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<(byte[] fileBytes, string contentType)?> GetFileContentAsync(int fileId, int userId)
        {
            var file = await GetFileByIdAsync(fileId, userId);
            if (file == null)
                return null;

            var filePath = Path.Combine(_uploadPath, file.FilePath);
            if (!File.Exists(filePath))
                return null;

            var fileBytes = await File.ReadAllBytesAsync(filePath);
            return (fileBytes, file.ContentType);
        }
    }
}