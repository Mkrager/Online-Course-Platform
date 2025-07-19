using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayPalOrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "TestAttempts",
                keyColumn: "Id",
                keyValue: new Guid("d45f7a9e-3a01-4c64-9f86-cde3e55ebc36"),
                column: "StartTime",
                value: new DateTime(2025, 7, 19, 16, 7, 46, 653, DateTimeKind.Local).AddTicks(8599));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CourseId",
                table: "Payments",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.UpdateData(
                table: "TestAttempts",
                keyColumn: "Id",
                keyValue: new Guid("d45f7a9e-3a01-4c64-9f86-cde3e55ebc36"),
                column: "StartTime",
                value: new DateTime(2025, 7, 16, 13, 51, 7, 618, DateTimeKind.Local).AddTicks(1251));
        }
    }
}
