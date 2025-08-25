using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using Ardalis.GuardClauses;
using Henshi.Flashcards.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Flashcards.Domain.Models;

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

    public static Flashcard Create(string question, string answer, Guid collectionId)
    {
        return new Flashcard(
            question,
            answer,
            Grade.VeryHard,
            DateTime.UtcNow,
            collectionId
        );
    }

    public void ReturnToLastGrade()
    {
        switch (Grade)
        {
            case Grade.VeryHard: break;
            case Grade.Hard: Grade = Grade.VeryHard; break;
            case Grade.Medium: Grade = Grade.Hard; break;
            case Grade.Easy: Grade = Grade.Medium; break;
            case Grade.VeryEasy: Grade = Grade.Easy; break;
            default: break;
        }
        NextRecall = CalculateNextRecall();
    }

    public void AdvanceToNextGrade()
    {
        switch (Grade)
        {
            case Grade.VeryHard: Grade = Grade.Hard; break;
            case Grade.Hard: Grade = Grade.Medium; break;
            case Grade.Medium: Grade = Grade.Easy; break;
            case Grade.Easy: Grade = Grade.VeryEasy; break;
            case Grade.VeryEasy: break;
            default: break;
        }
        NextRecall = CalculateNextRecall();
    }

    private DateTime CalculateNextRecall()
    {
        return Grade switch
        {
            Grade.VeryEasy => DateTime.UtcNow.AddDays(30),
            Grade.Easy => DateTime.UtcNow.AddDays(14),
            Grade.Medium => DateTime.UtcNow.AddDays(7),
            Grade.Hard => DateTime.UtcNow.AddDays(3),
            Grade.VeryHard => DateTime.UtcNow.AddDays(1),
            _ => DateTime.UtcNow
        };
    }
}
