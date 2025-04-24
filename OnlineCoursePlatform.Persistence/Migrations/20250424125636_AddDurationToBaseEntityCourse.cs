using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationToBaseEntityCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Courses",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                column: "Duration",
                value: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedDate", "Description", "Duration", "IsPublished", "LastModifiedBy", "LastModifiedDate", "Price", "ThumbnailUrl", "Title" },
                values: new object[] { new Guid("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"), new Guid("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a3"), "test2Id", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "test2", new TimeSpan(0, 0, 0, 0, 0), true, null, null, 100m, "test2", "test2" });

            migrationBuilder.InsertData(
                table: "Tests",
                columns: new[] { "Id", "CourseId", "Title" },
                values: new object[] { new Guid("1f5a4c21-2c9b-4b4e-bcb9-36b770a742d0"), new Guid("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"), "test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "Id",
                keyValue: new Guid("1f5a4c21-2c9b-4b4e-bcb9-36b770a742d0"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"));

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Courses");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedDate", "Description", "IsPublished", "LastModifiedBy", "LastModifiedDate", "Price", "ThumbnailUrl", "Title" },
                values: new object[] { new Guid("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"), new Guid("6f4c7e59-74c7-41c5-9fa7-4b75b7d9f3a3"), "test2Id", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "test2", true, null, null, 100m, "test2", "test2" });
        }
    }
}
