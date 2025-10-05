namespace Henshi.Flashcards.Flashcards.Features.ListFlashcards.V1;

public record ListFlashcardsQuery(
    Guid CollectionId,
    string? Search,
    int Page,
    int PageSize
);