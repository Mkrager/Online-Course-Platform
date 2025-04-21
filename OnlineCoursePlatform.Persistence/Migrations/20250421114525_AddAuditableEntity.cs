using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Courses",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a3"),
                columns: new[] { "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[] { new Guid("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a4"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "test2" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                columns: new[] { "CreatedBy", "LastModifiedBy", "LastModifiedDate" },
                values: new object[] { "testId", null, null });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedDate", "Description", "IsPublished", "LastModifiedBy", "LastModifiedDate", "Price", "ThumbnailUrl", "Title" },
                values: new object[] { new Guid("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"), new Guid("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a3"), "test2Id", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "test2", true, null, null, 100m, "test2", "test2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a4"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"));

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Courses",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "InstructorId",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                column: "InstructorId",
                value: "testId");
        }
    }
}
