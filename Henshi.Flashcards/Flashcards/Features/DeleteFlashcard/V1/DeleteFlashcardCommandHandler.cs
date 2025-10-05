using Henshi.Flashcards.Domain.Repositories;


namespace Henshi.Flashcards.Flashcards.Features.DeleteFlashcard.V1;

class DeleteFlashcardCommandHandler(IFlashcardRepository _flashcardRepository)
{
    public async Task Handle(DeleteFlashcardCommand query)
    {
        var flashcard = await _flashcardRepository.GetByIdAsync(query.Id);

        if (flashcard is null)
        {
            return;
        }

        _flashcardRepository.Delete(flashcard);
        await _flashcardRepository.SaveChangesAsync();
    }
}
