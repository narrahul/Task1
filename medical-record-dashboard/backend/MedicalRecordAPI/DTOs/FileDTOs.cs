using System.ComponentModel.DataAnnotations;

namespace MedicalRecordAPI.DTOs
{
    public class FileUploadDto
    {
        [Required]
        [RegularExpression("^(Lab Report|Prescription|X-Ray|Blood Report|MRI Scan|CT Scan)$", 
            ErrorMessage = "Invalid file type")]
        public string FileType { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(200)]
        public string FileName { get; set; } = string.Empty;
        
        [Required]
        public IFormFile File { get; set; } = null!;
    }

    public class FileResponseDto
    {
        public int Id { get; set; }
        public string FileType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}