using CVManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CVManagementSystem.Data
{
    public class CVContext:DbContext
    {
        public CVContext(DbContextOptions<CVContext> options) : base(options)
        {
        }

        public DbSet<CV> Cvs { get; set; }
        public DbSet<CVCollection> CVCollections { get; set; }
        public DbSet<JobSector> JobSectors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CVStatusType> CVStatusTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<EducationQualification> EducationQualifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CV>().ToTable("CV");
            modelBuilder.Entity<CVCollection>().ToTable("CVCollection");
            modelBuilder.Entity<JobSector>().ToTable("JobSector");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserStatus>().ToTable("UserStatus");
            modelBuilder.Entity<CVStatusType>().ToTable("CVStatusType");
            modelBuilder.Entity<EducationLevel>().ToTable("EducationLevel");
            modelBuilder.Entity<EducationQualification>().ToTable("EducationQualification");
        }

    }
}
