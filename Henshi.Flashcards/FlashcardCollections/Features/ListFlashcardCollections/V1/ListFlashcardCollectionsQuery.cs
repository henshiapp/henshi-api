namespace Henshi.Flashcards.FlashcardCollections.Features.ListFlashcardCollections.V1;

public record ListFlashcardCollectionsQuery(
    string? Search,
    int Page,
    int PageSize
);