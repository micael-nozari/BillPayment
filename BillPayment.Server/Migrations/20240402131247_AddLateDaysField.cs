using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillPayment.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddLateDaysField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LateDays",
                table: "AccountsPayable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AccountsPayable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "LateDays", "PaymentDate" },
                values: new object[] { new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Local), 0, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "AccountsPayable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DueDate", "LateDays", "PaymentDate" },
                values: new object[] { new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Local), 3, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LateDays",
                table: "AccountsPayable");

            migrationBuilder.UpdateData(
                table: "AccountsPayable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "PaymentDate" },
                values: new object[] { new DateTime(2024, 4, 4, 14, 59, 4, 392, DateTimeKind.Local).AddTicks(7728), new DateTime(2024, 4, 1, 14, 59, 4, 392, DateTimeKind.Local).AddTicks(7743) });

            migrationBuilder.UpdateData(
                table: "AccountsPayable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DueDate", "PaymentDate" },
                values: new object[] { new DateTime(2024, 3, 30, 14, 59, 4, 392, DateTimeKind.Local).AddTicks(7748), new DateTime(2024, 4, 2, 14, 59, 4, 392, DateTimeKind.Local).AddTicks(7749) });
        }
    }
}
