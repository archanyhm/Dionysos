using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dionysos.Migrations
{
    /// <inheritdoc />
    public partial class RenameDateToBestBefore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "InventoryItems",
                newName: "BestBefore");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BestBefore",
                table: "InventoryItems",
                newName: "Date");
        }
    }
}
