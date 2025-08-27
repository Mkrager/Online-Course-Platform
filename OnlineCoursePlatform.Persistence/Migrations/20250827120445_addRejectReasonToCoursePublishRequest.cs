using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addRejectReasonToCoursePublishRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursePublishRequest_Courses_CourseId",
                table: "CoursePublishRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePublishRequest",
                table: "CoursePublishRequest");

            migrationBuilder.RenameTable(
                name: "CoursePublishRequest",
                newName: "CoursePublishRequests");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePublishRequest_CourseId",
                table: "CoursePublishRequests",
                newName: "IX_CoursePublishRequests_CourseId");

            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                table: "CoursePublishRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePublishRequests",
                table: "CoursePublishRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePublishRequests_Courses_CourseId",
                table: "CoursePublishRequests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursePublishRequests_Courses_CourseId",
                table: "CoursePublishRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePublishRequests",
                table: "CoursePublishRequests");

            migrationBuilder.DropColumn(
                name: "RejectReason",
                table: "CoursePublishRequests");

            migrationBuilder.RenameTable(
                name: "CoursePublishRequests",
                newName: "CoursePublishRequest");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePublishRequests_CourseId",
                table: "CoursePublishRequest",
                newName: "IX_CoursePublishRequest_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePublishRequest",
                table: "CoursePublishRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePublishRequest_Courses_CourseId",
                table: "CoursePublishRequest",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
