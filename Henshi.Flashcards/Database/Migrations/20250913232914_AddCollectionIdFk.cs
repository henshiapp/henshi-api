using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Henshi.Flashcards.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectionIdFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_flashcards_collection_id",
                schema: "flashcards",
                table: "flashcards",
                column: "collection_id");

            migrationBuilder.AddForeignKey(
                name: "FK_flashcards_flashcard_collections_collection_id",
                schema: "flashcards",
                table: "flashcards",
                column: "collection_id",
                principalSchema: "flashcards",
                principalTable: "flashcard_collections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flashcards_flashcard_collections_collection_id",
                schema: "flashcards",
                table: "flashcards");

            migrationBuilder.DropIndex(
                name: "IX_flashcards_collection_id",
                schema: "flashcards",
                table: "flashcards");
        }
    }
}
