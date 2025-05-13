using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonIdToTestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Courses_CourseId",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Tests",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_CourseId",
                table: "Tests",
                newName: "IX_Tests_LessonId");

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "CreatedBy", "CreatedDate", "Description", "Duration", "LastModifiedBy", "LastModifiedDate", "Order", "Title", "VideoUrl" },
                values: new object[] { new Guid("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49"), new Guid("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new TimeSpan(0, 0, 0, 0, 0), null, null, 0, "test2Lesson", "" });

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "Id",
                keyValue: new Guid("1f5a4c21-2c9b-4b4e-bcb9-36b770a742d0"),
                column: "LessonId",
                value: new Guid("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49"));

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Lessons_LessonId",
                table: "Tests",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Lessons_LessonId",
                table: "Tests");

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("9c7f3d18-2c1e-4f37-9843-b25b6f1bfe49"));

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Tests",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_LessonId",
                table: "Tests",
                newName: "IX_Tests_CourseId");

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "Id",
                keyValue: new Guid("1f5a4c21-2c9b-4b4e-bcb9-36b770a742d0"),
                column: "CourseId",
                value: new Guid("7e1e9e74-905f-4ad6-8f8d-26ab9dd98ec1"));

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Courses_CourseId",
                table: "Tests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
