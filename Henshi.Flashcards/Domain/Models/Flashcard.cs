using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Flashcards.Domain;

[Table("flashcards")]
public class Flashcard
{
    [Key]
    [Column("id")]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Column("question")]
    public string Question { get; private set; } = string.Empty;

    [Column("answer")]
    public string Answer { get; private set; } = string.Empty;

    [Column("grade")]
    public Grade Grade { get; private set; }

    [Column("next_recall")]
    public DateTime NextRecall { get; private set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    [Column("collection_id")]
    public Guid CollectionId { get; private set; }

    public Flashcard(string question, string answer, Grade grade, DateTime nextRecall, Guid collectionId)
    {
        Question = Guard.Against.NullOrEmpty(question);
        Answer = Guard.Against.NullOrEmpty(answer);
        Grade = Guard.Against.Null(grade);
        NextRecall = Guard.Against.Null(nextRecall);
        CollectionId = Guard.Against.Null(collectionId);
    }
}
