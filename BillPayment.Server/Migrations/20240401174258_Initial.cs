using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillPayment.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountsPayable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    FinePercentage = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    FineAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    InterestPercentage = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    InterestAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsPayable", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsPayable");
        }
    }
}
