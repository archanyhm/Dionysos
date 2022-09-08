using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dionysos.Migrations
{
    /// <inheritdoc />
    public partial class FixedKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Articles_ArticleEan",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleEan",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleEan",
                table: "Articles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleEan",
                table: "Articles",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleEan",
                table: "Articles",
                column: "ArticleEan");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Articles_ArticleEan",
                table: "Articles",
                column: "ArticleEan",
                principalTable: "Articles",
                principalColumn: "Ean");
        }
    }
}
