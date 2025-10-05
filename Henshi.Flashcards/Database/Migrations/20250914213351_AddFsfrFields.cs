using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Henshi.Flashcards.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFsfrFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "difficulty",
                schema: "flashcards",
                table: "flashcards",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_recall",
                schema: "flashcards",
                table: "flashcards",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "stability",
                schema: "flashcards",
                table: "flashcards",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                schema: "flashcards",
                table: "flashcards",
                type: "text",
                nullable: false,
                defaultValue: "Learning");

            migrationBuilder.AddColumn<int>(
                name: "step",
                schema: "flashcards",
                table: "flashcards",
                type: "integer",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE flashcards.flashcards SET grade = 'Again', next_recall = NOW()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "difficulty",
                schema: "flashcards",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "last_recall",
                schema: "flashcards",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "stability",
                schema: "flashcards",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "state",
                schema: "flashcards",
                table: "flashcards");

            migrationBuilder.DropColumn(
                name: "step",
                schema: "flashcards",
                table: "flashcards");
        }
    }
}
