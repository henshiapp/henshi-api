namespace Henshi.Flashcards.FlashcardCollections.Features.CreateFlashcardCollection.V1;

public record CreateFlashcardCollectionRequest(
    string Title,
    string? Description,
    string Icon
);