using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStringOrderStatusToOrderStatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "TestAttempts",
                keyColumn: "Id",
                keyValue: new Guid("d45f7a9e-3a01-4c64-9f86-cde3e55ebc36"),
                column: "StartTime",
                value: new DateTime(2025, 7, 21, 15, 49, 43, 862, DateTimeKind.Local).AddTicks(5688));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "TestAttempts",
                keyColumn: "Id",
                keyValue: new Guid("d45f7a9e-3a01-4c64-9f86-cde3e55ebc36"),
                column: "StartTime",
                value: new DateTime(2025, 7, 19, 16, 7, 46, 653, DateTimeKind.Local).AddTicks(8599));
        }
    }
}
