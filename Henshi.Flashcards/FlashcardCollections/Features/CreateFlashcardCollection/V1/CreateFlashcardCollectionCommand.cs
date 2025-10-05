namespace Henshi.Flashcards.FlashcardCollections.Features.CreateFlashcardCollection.V1;

public record CreateFlashcardCollectionCommand(
    string Title,
    string? Description,
    string Icon,
    string UserId
);