using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Henshi.Flashcards.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveQuestionAndAnswerEncryption : Migration
    {
        /// <inheritdoc />
        protected override async void Up(MigrationBuilder migrationBuilder)
        {
            using (var context = new FlashcardDbContextFactory().CreateDbContext(null))
            {
                var provider = new GenerateEncryptionProvider(Environment.GetEnvironmentVariable("DATABASE_ENCRYPTION_KEY"));
                using var connection = context.Database.GetDbConnection();
                await connection.OpenAsync();
                
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT id, question, answer FROM flashcards.flashcards";
                
                var updates = new List<(Guid Id, string Question, string Answer)>();
                
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var id = reader.GetGuid(reader.GetOrdinal("id"));
                    var question = reader.GetString(reader.GetOrdinal("question"));
                    var answer = reader.GetString(reader.GetOrdinal("answer"));
                    
                    var decryptedQuestion = provider.Decrypt(question);
                    var decryptedAnswer = provider.Decrypt(answer);
                    updates.Add((id, decryptedQuestion, decryptedAnswer));
                }
                reader.Close();
                
                foreach (var (id, decryptedQuestion, decryptedAnswer) in updates)
                {
                    await context.Database.ExecuteSqlRawAsync(
                        "UPDATE flashcards.flashcards SET question = {0}, answer = {1} WHERE Id = {2}",
                        decryptedQuestion, decryptedAnswer, id);
                }
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
