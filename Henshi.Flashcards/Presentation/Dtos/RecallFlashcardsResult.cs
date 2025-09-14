using Henshi.Flashcards.Domain.ValueObjects;

namespace Henshi.Flashcards.Presentation.Dtos;

public record RecallFlashcardsResult(Guid FlashcardId, Grade Grade);