using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeStampedEntityToPaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TestAttempts",
                keyColumn: "Id",
                keyValue: new Guid("d45f7a9e-3a01-4c64-9f86-cde3e55ebc36"),
                column: "StartTime",
                value: new DateTime(2025, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "TestAttempts",
                keyColumn: "Id",
                keyValue: new Guid("d45f7a9e-3a01-4c64-9f86-cde3e55ebc36"),
                column: "StartTime",
                value: new DateTime(2025, 7, 21, 15, 49, 43, 862, DateTimeKind.Local).AddTicks(5688));
        }
    }
}
