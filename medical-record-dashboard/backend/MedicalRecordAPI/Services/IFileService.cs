using MedicalRecordAPI.DTOs;
using MedicalRecordAPI.Models;

namespace MedicalRecordAPI.Services
{
    public interface IFileService
    {
        Task<MedicalFile?> UploadFileAsync(int userId, FileUploadDto fileDto);
        Task<IEnumerable<MedicalFile>> GetUserFilesAsync(int userId);
        Task<MedicalFile?> GetFileByIdAsync(int fileId, int userId);
        Task<bool> DeleteFileAsync(int fileId, int userId);
        Task<(byte[] fileBytes, string contentType)?> GetFileContentAsync(int fileId, int userId);
    }
}