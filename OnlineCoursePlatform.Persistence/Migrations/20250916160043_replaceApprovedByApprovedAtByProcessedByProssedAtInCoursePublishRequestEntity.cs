using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class replaceApprovedByApprovedAtByProcessedByProssedAtInCoursePublishRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApprovedBy",
                table: "CoursePublishRequests",
                newName: "ProcessedBy");

            migrationBuilder.RenameColumn(
                name: "ApprovedAt",
                table: "CoursePublishRequests",
                newName: "ProcessedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcessedBy",
                table: "CoursePublishRequests",
                newName: "ApprovedBy");

            migrationBuilder.RenameColumn(
                name: "ProcessedAt",
                table: "CoursePublishRequests",
                newName: "ApprovedAt");
        }
    }
}
