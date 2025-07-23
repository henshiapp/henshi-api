using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;

namespace Henshi.Flashcards.Domain.Models;

[Table("flashcard_collections")]
public class FlashcardCollection
{
    [Key]
    [Column("id")]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Column("title")]
    public string Title { get; private set; } = string.Empty;

    [Column("description")]
    public string? Description { get; private set; }

    [Column("icon")]
    public string Icon { get; private set; } = string.Empty;

    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    public readonly IReadOnlyList<Flashcard> Flashcards = [];

    public FlashcardCollection(string title, string? description, string icon, string userId)
    {
        Title = Guard.Against.NullOrEmpty(title);
        Description = description;
        Icon = Guard.Against.NullOrEmpty(icon);
        UserId = Guard.Against.NullOrEmpty(userId);
    }
}
