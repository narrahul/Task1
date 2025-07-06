using System.ComponentModel.DataAnnotations;

namespace MedicalRecordAPI.Models
{
    public class MedicalFile
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string FileType { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(200)]
        public string FileName { get; set; } = string.Empty;
        
        [Required]
        public string FilePath { get; set; } = string.Empty;
        
        [Required]
        public string ContentType { get; set; } = string.Empty;
        
        public long FileSize { get; set; }
        
        public DateTime UploadedAt { get; set; }
        
        public User User { get; set; } = null!;
    }
}