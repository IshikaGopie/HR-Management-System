using Microsoft.EntityFrameworkCore;
using HRPlusAssignment.Models;

namespace HRPlusAssignment.Data
{
    public class HrDbContext : DbContext
    {
        public HrDbContext(DbContextOptions<HrDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<JobGroup> JobGroups { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Department
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);
                entity.Property(e => e.DepartmentId).HasMaxLength(50);
                entity.Property(e => e.DepartmentCode).HasMaxLength(50);
                entity.Property(e => e.DepartmentName).HasMaxLength(100);
            });

            // Configure JobGroup
            modelBuilder.Entity<JobGroup>(entity =>
            {
                entity.HasKey(e => e.JobGroupId);
                entity.Property(e => e.JobGroupId).HasMaxLength(50);
                entity.Property(e => e.JobGroupName).HasMaxLength(100);
            });

            // Configure Job
            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobId);
                entity.Property(e => e.JobId).HasMaxLength(50);
                entity.Property(e => e.JobCode).HasMaxLength(50);
                entity.Property(e => e.JobTitle).HasMaxLength(100);
                entity.Property(e => e.JobGroupId).HasMaxLength(50);

                entity.HasOne(d => d.JobGroup)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.JobGroupId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Position
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.PositionId);
                entity.Property(e => e.PositionId).HasMaxLength(50);
                entity.Property(e => e.PositionCode).HasMaxLength(50);
                entity.Property(e => e.PositionTitle).HasMaxLength(100);
                entity.Property(e => e.DepartmentId).HasMaxLength(50);
                entity.Property(e => e.JobId).HasMaxLength(50);
                entity.Property(e => e.JobLevel).HasMaxLength(50);
                entity.Property(e => e.ReportsToPositionId).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ReportsToPosition)
                    .WithMany(p => p.SubordinatePositions)
                    .HasForeignKey(d => d.ReportsToPositionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);
                entity.Property(e => e.EmployeeId).HasMaxLength(50);
                entity.Property(e => e.PositionId).HasMaxLength(50);
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}