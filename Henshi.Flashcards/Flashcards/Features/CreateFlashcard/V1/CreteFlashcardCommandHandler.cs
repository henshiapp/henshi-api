using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Flashcards.Models;

namespace Henshi.Flashcards.Flashcards.Features.CreateFlashcard.V1;

public class CreateFlashcardCommandHandler(IFlashcardRepository _flashcardRepository)
{
    public async Task Handle(CreateFlashcardCommand command)
    {
        var flashcard = Flashcard.Create(command.Question, command.Answer, command.CollectionId);

        await _flashcardRepository.AddAsync(flashcard);
        await _flashcardRepository.SaveChangesAsync();
    }
}
