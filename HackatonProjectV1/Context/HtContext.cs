 using HackatonProjectV1.Entities;
using HackatonProjectV1.Entities.MainPageElements;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.Context
{
    public class HtContext : IdentityDbContext<AppUser, IdentityRole , string>
    {
        public HtContext(DbContextOptions<HtContext> options) : base(options)
        { 

        }

        public DbSet<Content> contents { get; set; }
        public DbSet<Comments> comments { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Faculty> faculties { get; set; }
        public DbSet<University> universities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // UNIVERSITY → FACULTY (cascade olabilir)
            builder.Entity<Faculty>()
                .HasOne(f => f.University)
                .WithMany(u => u.faculties)
                .HasForeignKey(f => f.UniversityId)
                .OnDelete(DeleteBehavior.Restrict); // EN GÜVENLİ

            // FACULTY → DEPARTMENT
            builder.Entity<Department>()
                .HasOne(d => d.Faculty)
                .WithMany(f => f.departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            // CONTENT → UNIVERSITY (nullable)
            builder.Entity<Content>()
                .HasOne(c => c.university)
                .WithMany(u => u.contents)
                .HasForeignKey(c => c.UniversityId)
                .OnDelete(DeleteBehavior.SetNull);

            // CONTENT → FACULTY (nullable)
            builder.Entity<Content>()
                .HasOne(c => c.faculty)
                .WithMany(f => f.contents)
                .HasForeignKey(c => c.FacultyId)
                .OnDelete(DeleteBehavior.SetNull);

            // CONTENT → DEPARTMENT (nullable)
            builder.Entity<Content>()
                .HasOne(c => c.department)
                .WithMany(d => d.contents)
                .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
