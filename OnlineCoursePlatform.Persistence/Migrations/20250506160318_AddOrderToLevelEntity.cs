using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderToLevelEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Levels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "CreatedBy", "CreatedDate", "Description", "Duration", "LastModifiedBy", "LastModifiedDate", "Order", "Title", "VideoUrl" },
                values: new object[] { new Guid("2e8b13d5-4c5e-4f4b-9387-8e19c844dbe9"), new Guid("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new TimeSpan(0, 0, 0, 0, 0), null, null, 0, "testLesson", "" });

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("03e986cf-2784-4096-b130-2762c2018777"),
                column: "Order",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("3503ccbe-92df-4525-a908-a4aceeae1036"),
                column: "Order",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("f80e97ef-6640-41a5-8ccd-603a6ab1bd33"),
                column: "Order",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("2e8b13d5-4c5e-4f4b-9387-8e19c844dbe9"));

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Levels");
        }
    }
}
