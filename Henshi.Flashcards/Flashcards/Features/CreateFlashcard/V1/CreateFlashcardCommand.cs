namespace Henshi.Flashcards.Flashcards.Features.CreateFlashcard.V1;

public record CreateFlashcardCommand(
    Guid CollectionId,
    string Question,
    string Answer
);