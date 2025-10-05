using Henshi.Flashcards.Flashcards.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Henshi.Flashcards.Database;

public class FlashcardConfiguration : IEntityTypeConfiguration<Flashcard>
{
    public void Configure(EntityTypeBuilder<Flashcard> builder)
    {

    }
}
