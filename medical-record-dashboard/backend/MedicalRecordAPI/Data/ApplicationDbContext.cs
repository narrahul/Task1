using Microsoft.EntityFrameworkCore;
using MedicalRecordAPI.Models;

namespace MedicalRecordAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MedicalFile> MedicalFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<MedicalFile>(entity =>
            {
                entity.Property(e => e.UploadedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                
                entity.HasOne(d => d.User)
                    .WithMany(p => p.MedicalFiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}