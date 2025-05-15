using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addAuditableEntitytoTestbaseentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Tests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Tests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "Id",
                keyValue: new Guid("1f5a4c21-2c9b-4b4e-bcb9-36b770a742d0"),
                columns: new[] { "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.InsertData(
                table: "Tests",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "LessonId", "Title" },
                values: new object[] { new Guid("4a8c1a3f-7e1c-49d3-9bc1-1f8b38f1f3aa"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49"), "test2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "Id",
                keyValue: new Guid("4a8c1a3f-7e1c-49d3-9bc1-1f8b38f1f3aa"));

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Tests");
        }
    }
}
