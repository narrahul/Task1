using Microsoft.EntityFrameworkCore;

namespace MedicalRecordAPI.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();
            
            // Create tables using raw SQL if they don't exist
            var createUsersTable = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    FullName VARCHAR(100) NOT NULL,
                    Email VARCHAR(100) NOT NULL UNIQUE,
                    Gender VARCHAR(10) NOT NULL,
                    PhoneNumber VARCHAR(20) NOT NULL,
                    PasswordHash VARCHAR(255) NOT NULL,
                    ProfileImagePath VARCHAR(500),
                    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                    INDEX idx_email (Email)
                );";
            
            var createMedicalFilesTable = @"
                CREATE TABLE IF NOT EXISTS MedicalFiles (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    UserId INT NOT NULL,
                    FileType VARCHAR(50) NOT NULL,
                    FileName VARCHAR(200) NOT NULL,
                    FilePath VARCHAR(500) NOT NULL,
                    ContentType VARCHAR(100) NOT NULL,
                    FileSize BIGINT NOT NULL,
                    UploadedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
                    INDEX idx_user_id (UserId),
                    INDEX idx_uploaded_at (UploadedAt)
                );";
            
            try
            {
                context.Database.ExecuteSqlRaw(createUsersTable);
                context.Database.ExecuteSqlRaw(createMedicalFilesTable);
            }
            catch (Exception ex)
            {
                // Tables might already exist, which is fine
                Console.WriteLine($"Database initialization note: {ex.Message}");
            }
        }
    }
}