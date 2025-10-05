using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Henshi.Flashcards.Domain.ValueObjects;
using Henshi.Flashcards.FlashcardCollections.Models;

namespace Henshi.Flashcards.Flashcards.Models;

[Table("flashcards")]
public class Flashcard
{
    [Key]
    [Column("id")]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Column("question")]
    public string Question { get; set; } = string.Empty;

    [Column("answer")]
    public string Answer { get; set; } = string.Empty;

    [Column("grade")]
    public Grade Grade { get; private set; } = Grade.Again;

    [Column("state")]
    public State State { get; private set; } = State.Learning;

    [Column("step")]
    public int? Step { get; private set; } = 0;

    [Column("stability")]
    public double? Stability { get; private set; }

    [Column("difficulty")]
    public double? Difficulty { get; private set; }

    [Column("next_recall")]
    public DateTime NextRecall { get; private set; }

    [Column("last_recall")]
    public DateTime? LastRecall { get; private set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    [Column("collection_id")]
    [ForeignKey("Collection")]
    public Guid CollectionId { get; private set; }

    public virtual FlashcardCollection Collection { get; set; }

    public Flashcard(
        string question,
        string answer,
        Grade grade,
        State state,
        int? step,
        double? stability,
        double? difficulty,
        DateTime nextRecall,
        DateTime? lastRecall,
        Guid collectionId
    )
    {
        Question = Guard.Against.NullOrEmpty(question);
        Answer = Guard.Against.NullOrEmpty(answer);
        Grade = Guard.Against.Null(grade);
        State = Guard.Against.Null(state);
        Step = step;
        Stability = stability;
        Difficulty = difficulty;
        NextRecall = Guard.Against.Null(nextRecall);
        LastRecall = lastRecall;
        CollectionId = Guard.Against.Null(collectionId);
    }

    public static Flashcard Create(string question, string answer, Guid collectionId)
    {
        return new Flashcard(
            question,
            answer,
            Grade.Again,
            State.Learning,
            null,
            null,
            null,
            DateTime.UtcNow,
            null,
            collectionId
        );
    }

    public void SaveRecallInformation(DateTime nextRecall, Grade grade, State state, int? step, double? stability, double? difficulty)
    {
        NextRecall = nextRecall;
        LastRecall = DateTime.UtcNow;
        Step = step;
        Stability = stability;
        Difficulty = difficulty;
        Grade = grade;
        State = state;
    }
}
