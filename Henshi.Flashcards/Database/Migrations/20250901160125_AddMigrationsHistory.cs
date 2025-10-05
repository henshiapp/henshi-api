using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Henshi.Flashcards.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationsHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "flashcards");

            migrationBuilder.CreateTable(
                name: "__EFMigrationsHistory",
                schema: "flashcards",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "varchar", maxLength: 150, nullable: false),
                    ProductVersion = table.Column<string>(type: "varchar", maxLength: 32, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFMigrationsHistory", x => x.MigrationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "__EFMigrationsHistory",
                schema: "flashcards");
        }
    }
}
