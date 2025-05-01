using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagementSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_AdminUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AdminUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "Id", "EMail", "HashedPassword" },
                values: new object[] { new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), "oktay@acerpro.com", "$2a$11$dGw/NbhJeg/XPqBuFU0NE.uUuwcG/nKF6P/RpJoqZsGUF7Q5IARue" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "DepartmentName", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("2a17a3a7-c0c5-46ab-b84a-9db273383b32"), new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "İnsan Kaynakları", false, new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2a17a3a7-c0c5-46ab-b84a-9db273383b33"), new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Muhasebe", false, new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2a17a3a7-c0c5-46ab-b84a-9db273383b36"), new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Yazılım", false, new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Adress", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "DepartmentId", "EMail", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "Phone", "SurName" },
                values: new object[,]
                {
                    { new Guid("46dcdc1c-cc52-478d-b88a-684c01c3ac22"), "Bağcılar İstanbul", new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("2a17a3a7-c0c5-46ab-b84a-9db273383b33"), "perihan@acerpro.com", false, new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Perihan", "0530 123 45 69", "Gülmez" },
                    { new Guid("46dcdc1c-cc52-478d-b88a-684c01c3ac23"), "Bağcılar İstanbul", new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("2a17a3a7-c0c5-46ab-b84a-9db273383b36"), "admin@acerpro.com", false, new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Berkay", "0530 123 45 69", "Gülmez" },
                    { new Guid("46dcdc1c-cc52-478d-b88a-684c01c3ac24"), "Esenler İstanbul", new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("2a17a3a7-c0c5-46ab-b84a-9db273383b32"), "ilknur@acerpro.com", false, new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "İlknur", "0530 123 45 69", "Acer" },
                    { new Guid("46dcdc1c-cc52-478d-b88a-684c01c3ac25"), "Esenler İstanbul", new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("2a17a3a7-c0c5-46ab-b84a-9db273383b36"), "aytul@acerpro.com", false, new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aytül", "0530 123 45 69", "Peker" },
                    { new Guid("46dcdc1c-cc52-478d-b88a-684c01c3ac26"), "Bağcılar İstanbul", new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("2a17a3a7-c0c5-46ab-b84a-9db273383b36"), "admin@acerpro.com", false, new Guid("f47c9f2e-e48b-4b5f-a897-9997ab0a7e4a"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oktay", "0530 123 45 69", "Gülmez" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CreatedBy",
                table: "Departments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CreatedBy",
                table: "Employees",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "AdminUsers");
        }
    }
}
