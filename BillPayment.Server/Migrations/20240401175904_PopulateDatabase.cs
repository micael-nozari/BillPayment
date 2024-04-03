using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BillPayment.Server.Migrations
{
    /// <inheritdoc />
    public partial class PopulateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccountsPayable",
                columns: new[] { "Id", "DueDate", "FineAmount", "FinePercentage", "InterestAmount", "InterestPercentage", "Name", "OriginalAmount", "PaymentDate", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 4, 14, 59, 4, 392, DateTimeKind.Local).AddTicks(7728), 0m, 0m, 0m, 0m, "Light", 100m, new DateTime(2024, 4, 1, 14, 59, 4, 392, DateTimeKind.Local).AddTicks(7743), 100m },
                    { 2, new DateTime(2024, 3, 30, 14, 59, 4, 392, DateTimeKind.Local).AddTicks(7748), 1m, 2m, 0.15m, 0.1m, "Water", 50m, new DateTime(2024, 4, 2, 14, 59, 4, 392, DateTimeKind.Local).AddTicks(7749), 51.15m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountsPayable",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AccountsPayable",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
