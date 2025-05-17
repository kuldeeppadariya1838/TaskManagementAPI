using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TMUsers> TMUsers { get; set; }
        public DbSet<TMTasks> TMTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TMUsers>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(u => u.Password)
                    .IsRequired();
                entity.Property(u => u.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(u => u.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // Relationship with Tasks
                entity.HasMany(u => u.TMTasks)
                    .WithOne(t => t.TMUsers)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure the Task entity
            modelBuilder.Entity<TMTasks>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(t => t.Description)
                    .HasMaxLength(500);
                entity.Property(t => t.Priority)
                    .IsRequired()
                    .HasConversion<string>();
                entity.Property(t => t.Status)
                    .IsRequired()
                    .HasConversion<string>();
                entity.Property(t => t.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(t => t.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
