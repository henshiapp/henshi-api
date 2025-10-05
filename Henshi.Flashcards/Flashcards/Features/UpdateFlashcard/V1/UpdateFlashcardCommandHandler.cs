using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Flashcards.Models;


namespace Henshi.Flashcards.Flashcards.Features.CreateFlashcard.V1;

class UpdateFlashcardCommandHandler(IFlashcardRepository _flashcardRepository)
{
    public async Task<Flashcard?> Handle(UpdateFlashcardCommand command)
    {
        var flashcard = await _flashcardRepository.GetByIdAsync(command.Id);

        if (flashcard is null) return null;

        flashcard.Question = command.Question;
        flashcard.Answer = command.Answer;

        await _flashcardRepository.SaveChangesAsync();

        return flashcard;
    }
}
