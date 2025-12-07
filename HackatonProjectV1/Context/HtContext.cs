
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

            // --- İLİŞKİ YAPILANDIRMALARI (FLUENT API) ---

            // 1. UNIVERSITY -> FACULTY İlişkisi
            // Bir üniversite silinirse, fakülteleri silinmesin (Hata önlemek için Restrict)
            builder.Entity<Faculty>()
                .HasOne(f => f.University)
                .WithMany(u => u.faculties)
                .HasForeignKey(f => f.UniversityId)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. UNIVERSITY -> DEPARTMENT İlişkisi (HATAYI ÇÖZEN KISIM)
            // Bir üniversite silinirse, bölümleri silinmesin. 
            // Bu ilişki Faculty üzerinden de sağlandığı için "Cycle" hatası veriyordu, Restrict ile çözüldü.
            builder.Entity<Department>()
                .HasOne(d => d.University)
                .WithMany(u => u.departments)
                .HasForeignKey(d => d.UniversityId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. FACULTY -> DEPARTMENT İlişkisi
            // Bir fakülte silinirse, bölümleri silinmesin.
            builder.Entity<Department>()
                .HasOne(d => d.Faculty)
                .WithMany(f => f.departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            // 4. CONTENT İLİŞKİLERİ (SetNull - İçerik silinmesin, sadece bağ kopsun)

            // CONTENT -> UNIVERSITY
            builder.Entity<Content>()
                .HasOne(c => c.university)
                .WithMany(u => u.contents)
                .HasForeignKey(c => c.UniversityId)
                .OnDelete(DeleteBehavior.SetNull);

            // CONTENT -> FACULTY
            builder.Entity<Content>()
                .HasOne(c => c.faculty)
                .WithMany(f => f.contents)
                .HasForeignKey(c => c.FacultyId)
                .OnDelete(DeleteBehavior.SetNull);

            // CONTENT -> DEPARTMENT
            // Not: Content entity'sinde Department için foreign key 'Id' olarak maplenmiş görünüyor (önceki koddan).
            // Genelde 'DepartmentId' olması beklenir ama mevcut yapıyı bozmuyoruz.
            builder.Entity<Content>()
                .HasOne(c => c.department)
                .WithMany(d => d.contents)
                .HasForeignKey(c => c.departmentId) 
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
