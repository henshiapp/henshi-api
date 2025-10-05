namespace Henshi.Flashcards.FlashcardCollections.Features.UpdateFlashcardCollection.V1;

public record UpdateFlashcardCollectionCommand(
    Guid Id,
    string Title,
    string? Description,
    string Icon
);