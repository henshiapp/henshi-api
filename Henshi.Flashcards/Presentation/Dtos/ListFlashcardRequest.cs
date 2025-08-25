namespace Henshi.Flashcards.Presentation.Dtos;

public record ListFlashcardRequest(string? Search) : PaginationRequest;