namespace Henshi.Flashcards.Presentation.Dtos;

public record ListFlashcardCollectionRequest(string? Search) : PaginationRequest;