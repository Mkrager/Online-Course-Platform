using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentsIdToEnrollmentEtity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "TestId", "Text" },
                values: new object[] { new Guid("a1783ff1-7a2b-4d7a-84a5-c453be4c0f90"), new Guid("4a8c1a3f-7e1c-49d3-9bc1-1f8b38f1f3aa"), "someQuestion" });

            migrationBuilder.InsertData(
                table: "TestAttempts",
                columns: new[] { "Id", "EndTime", "IsCompleted", "StartTime", "TestId", "UserId" },
                values: new object[] { new Guid("d45f7a9e-3a01-4c64-9f86-cde3e55ebc36"), null, false, new DateTime(2025, 7, 16, 13, 51, 7, 618, DateTimeKind.Local).AddTicks(1251), new Guid("4a8c1a3f-7e1c-49d3-9bc1-1f8b38f1f3aa"), "someUserId" });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "IsCorrect", "QuestionId", "Text" },
                values: new object[] { new Guid("5cd711f0-cc43-4b7f-b6a3-d7f4c208b38a"), true, new Guid("a1783ff1-7a2b-4d7a-84a5-c453be4c0f90"), "someAnswer" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("5cd711f0-cc43-4b7f-b6a3-d7f4c208b38a"));

            migrationBuilder.DeleteData(
                table: "TestAttempts",
                keyColumn: "Id",
                keyValue: new Guid("d45f7a9e-3a01-4c64-9f86-cde3e55ebc36"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("a1783ff1-7a2b-4d7a-84a5-c453be4c0f90"));

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Enrollments");
        }
    }
}
