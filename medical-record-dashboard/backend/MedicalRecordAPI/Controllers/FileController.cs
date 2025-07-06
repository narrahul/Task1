using Microsoft.AspNetCore.Mvc;
using MedicalRecordAPI.DTOs;
using MedicalRecordAPI.Services;

namespace MedicalRecordAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDto fileDto)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
                return Unauthorized(new { message = "Not authenticated" });

            var file = await _fileService.UploadFileAsync(userId.Value, fileDto);
            
            if (file == null)
                return BadRequest(new { message = "Invalid file type or file size exceeds limit" });

            return Ok(new FileResponseDto
            {
                Id = file.Id,
                FileType = file.FileType,
                FileName = file.FileName,
                FilePath = file.FilePath,
                ContentType = file.ContentType,
                FileSize = file.FileSize,
                UploadedAt = file.UploadedAt
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFiles()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
                return Unauthorized(new { message = "Not authenticated" });

            var files = await _fileService.GetUserFilesAsync(userId.Value);
            
            var response = files.Select(f => new FileResponseDto
            {
                Id = f.Id,
                FileType = f.FileType,
                FileName = f.FileName,
                FilePath = f.FilePath,
                ContentType = f.ContentType,
                FileSize = f.FileSize,
                UploadedAt = f.UploadedAt
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
                return Unauthorized(new { message = "Not authenticated" });

            var result = await _fileService.GetFileContentAsync(id, userId.Value);
            
            if (result == null)
                return NotFound(new { message = "File not found" });

            return File(result.Value.fileBytes, result.Value.contentType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
                return Unauthorized(new { message = "Not authenticated" });

            var result = await _fileService.DeleteFileAsync(id, userId.Value);
            
            if (!result)
                return NotFound(new { message = "File not found" });

            return Ok(new { message = "File deleted successfully" });
        }
    }
}