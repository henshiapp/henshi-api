using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Ardalis.GuardClauses;

namespace Henshi.Flashcards.Domain.Models;

[Table("flashcard_collections")]
public class FlashcardCollection
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }

    [Column("icon")]
    public string Icon { get; set; } = string.Empty;

    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public virtual ICollection<Flashcard> Flashcards { get; set; }

    public FlashcardCollection(string title, string? description, string icon, string userId)
    {
        Title = Guard.Against.NullOrEmpty(title);
        Description = description;
        Icon = Guard.Against.NullOrEmpty(icon);
        UserId = Guard.Against.NullOrEmpty(userId);
    }
}
