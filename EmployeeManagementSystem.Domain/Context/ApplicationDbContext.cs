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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Department>()
                .Property(d => d.DepartmentName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(e => e.SurName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EMail)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone)
                .HasMaxLength(20);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Adress)
                .HasMaxLength(250);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);


            // Örnek veriler
            var AdminUserId = Guid.Parse("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a");

            var departmentId1 = Guid.Parse("2a17a3a7-c0c5-46ab-b84a-9db273383b36");
            var departmentId2 = Guid.Parse("2a17a3a7-c0c5-46ab-b84a-9db273383b32");
            var departmentId3 = Guid.Parse("2a17a3a7-c0c5-46ab-b84a-9db273383b33");

            var employeeId1 = Guid.Parse("46dcdc1c-cc52-478d-b88a-684c01c3ac26");
            var employeeId2 = Guid.Parse("46dcdc1c-cc52-478d-b88a-684c01c3ac25");
            var employeeId3 = Guid.Parse("46dcdc1c-cc52-478d-b88a-684c01c3ac24");
            var employeeId4 = Guid.Parse("46dcdc1c-cc52-478d-b88a-684c01c3ac23");
            var employeeId5 = Guid.Parse("46dcdc1c-cc52-478d-b88a-684c01c3ac22");

            var seedDate = new DateTime(2025, 05, 02);

            modelBuilder.Entity<AdminUser>().HasData(new AdminUser
            {
                Id = AdminUserId,
                EMail = "oktay@acerpro.com",
                HashedPassword = "$2a$11$dGw/NbhJeg/XPqBuFU0NE.uUuwcG/nKF6P/RpJoqZsGUF7Q5IARue",  //123456
            });

            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = departmentId1,
                DepartmentName = "Yazılım",
                CreatedDate = seedDate,
                CreatedBy = AdminUserId,
                ModifiedDate = seedDate,
                ModifiedBy = AdminUserId,
                IsDeleted = false
            });

            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = departmentId2,
                DepartmentName = "İnsan Kaynakları",
                CreatedDate = seedDate,
                CreatedBy = AdminUserId,
                ModifiedDate = seedDate,
                ModifiedBy = AdminUserId,
                IsDeleted = false
            });

            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = departmentId3,
                DepartmentName = "Muhasebe",
                CreatedDate = seedDate,
                CreatedBy = AdminUserId,
                ModifiedDate = seedDate,
                ModifiedBy = AdminUserId,
                IsDeleted = false
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = employeeId1,
                Name = "Oktay",
                SurName = "Gülmez",
                EMail = "admin@acerpro.com",
                Phone="0530 123 45 69",
                Adress="Bağcılar İstanbul",
                DepartmentId = departmentId1,
                CreatedDate = seedDate,
                CreatedBy = AdminUserId,
                ModifiedDate = seedDate,
                ModifiedBy = AdminUserId,
                IsDeleted = false
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = employeeId2,
                Name = "Aytül",
                SurName = "Peker",
                EMail = "aytul@acerpro.com",
                Phone = "0530 123 45 69",
                Adress = "Esenler İstanbul",
                DepartmentId = departmentId1,
                CreatedDate = seedDate,
                CreatedBy = AdminUserId,
                ModifiedDate = seedDate,
                ModifiedBy = AdminUserId,
                IsDeleted = false
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = employeeId3,
                Name = "İlknur",
                SurName = "Acer",
                EMail = "ilknur@acerpro.com",
                Phone = "0530 123 45 69",
                Adress = "Esenler İstanbul",
                DepartmentId = departmentId2,
                CreatedDate = seedDate,
                CreatedBy = AdminUserId,
                ModifiedDate = seedDate,
                ModifiedBy = AdminUserId,
                IsDeleted = false
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = employeeId4,
                Name = "Berkay",
                SurName = "Gülmez",
                EMail = "admin@acerpro.com",
                Phone = "0530 123 45 69",
                Adress = "Bağcılar İstanbul",
                DepartmentId = departmentId1,
                CreatedDate = seedDate,
                CreatedBy = AdminUserId,
                ModifiedDate = seedDate,
                ModifiedBy = AdminUserId,
                IsDeleted = false
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = employeeId5,
                Name = "Perihan",
                SurName = "Gülmez",
                EMail = "perihan@acerpro.com",
                Phone = "0530 123 45 69",
                Adress = "Bağcılar İstanbul",
                DepartmentId = departmentId3,
                CreatedDate = seedDate,
                CreatedBy = AdminUserId,
                ModifiedDate = seedDate,
                ModifiedBy = AdminUserId,
                IsDeleted = false
            });


        }
    }
}
