using EmployeeManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Department entity
            modelBuilder.Entity<Department>()
                .HasKey(d => d.Id); // Id primary key

            modelBuilder.Entity<Department>()
                .Property(d => d.DepatmentName)
                .IsRequired()
                .HasMaxLength(100); // Set maximum length for DepartmentName

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees) // One-to-many relationship with Employee
                .WithOne(e => e.Department) // Employee has one Department
                .HasForeignKey(e => e.DepartmentId) // Foreign key in Employee
                .OnDelete(DeleteBehavior.NoAction); // Avoid cascade delete

            // Employee entity
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id); // Id primary key

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50); // Set maximum length for Name

            modelBuilder.Entity<Employee>()
                .Property(e => e.SurName)
                .IsRequired()
                .HasMaxLength(50); // Set maximum length for SurName

            modelBuilder.Entity<Employee>()
                .Property(e => e.EMail)
                .IsRequired()
                .HasMaxLength(100); // Set maximum length for Email

            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone)
                .HasMaxLength(20); // Set maximum length for Phone

            modelBuilder.Entity<Employee>()
                .Property(e => e.Adress)
                .HasMaxLength(250); // Set maximum length for Address

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department) // Each Employee has one Department
                .WithMany(d => d.Employees) // One Department can have many Employees
                .HasForeignKey(e => e.DepartmentId) // Foreign key in Employee
                .OnDelete(DeleteBehavior.NoAction); // Avoid cascade delete



        modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Logs");

                entity.Property(e => e.Message).HasColumnType("nvarchar(max)");
                entity.Property(e => e.MessageTemplate).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Level).HasMaxLength(128);
                entity.Property(e => e.TimeStamp).HasColumnType("datetimeoffset");
                entity.Property(e => e.Exception).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Properties).HasColumnType("nvarchar(max)");
                entity.Property(e => e.LogEvent).HasColumnType("nvarchar(max)");
            });

            // Seed data
            var AdminUserId = Guid.Parse("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a");
            var departmentId = Guid.Parse("2a17a3a7-c0c5-46ab-b84a-9db273383b36");
            var employeeId = Guid.Parse("46dcdc1c-cc52-478d-b88a-684c01c3ac26");
            var seedDate = new DateTime(2023, 1, 1);

            modelBuilder.Entity<AdminUser>().HasData(new AdminUser
            {
                Id = AdminUserId,
                EMail = "Yazılım",
                HashedPassword = "$2a$11$46jtcA9VHkgV0jNqyzC98OZ65PRNNZVf.n/8pcxo.ObnqI1z95MzW",
            });

            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = departmentId,
                DepatmentName = "Yazılım",
                CreatedDate = seedDate,
                CreatedBy = AdminUserId,
                ModifiedDate = seedDate,
                ModifiedBy = AdminUserId,
                IsDeleted = false
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = employeeId,
                Name = "Oktay",
                SurName = "Gülmez",
                EMail = "admin@acerpro.com",
                Phone="0530 123 45 69",
                Adress="Esenler İstanbul",
                DepartmentId = departmentId,
                CreatedDate = seedDate,
                CreatedBy = AdminUserId,
                ModifiedDate = seedDate,
                ModifiedBy = AdminUserId,
                IsDeleted = false
            });

           

        }
    }
}
