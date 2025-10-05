namespace Henshi.Flashcards.Flashcards.Features.CreateFlashcard.V1;

public record UpdateFlashcardCommand(Guid Id, string Question, string Answer);