using Henshi.Flashcards.Domain.ValueObjects;

namespace Henshi.Flashcards.Flashcards.Features.SaveRecall.V1;

public record RecallFlashcardsResult(Guid FlashcardId, Grade Grade);