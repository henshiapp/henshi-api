namespace Henshi.Flashcards.FlashcardCollections.Features.UpdateFlashcardCollection.V1;

public record UpdateFlashcardCollectionRequest(
    string Title,
    string? Description,
    string Icon
);