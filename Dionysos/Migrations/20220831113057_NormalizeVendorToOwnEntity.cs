using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dionysos.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeVendorToOwnEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vendor",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Articles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CountryCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_VendorId",
                table: "Articles",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Vendor_VendorId",
                table: "Articles",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Vendor_VendorId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Articles_VendorId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                table: "Articles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
