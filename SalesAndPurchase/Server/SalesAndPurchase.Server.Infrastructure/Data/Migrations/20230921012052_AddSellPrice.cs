using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesAndPurchase.Server.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSellPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SellPrice",
                schema: "sample",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellPrice",
                schema: "sample",
                table: "Product");
        }
    }
}
