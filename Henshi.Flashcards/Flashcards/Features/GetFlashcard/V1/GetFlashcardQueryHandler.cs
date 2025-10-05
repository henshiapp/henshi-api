using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Flashcards.Models;


namespace Henshi.Flashcards.Flashcards.Features.GetFlashcard.V1;

class GetFlashcardQueryHandler(IFlashcardRepository _flashcardRepository)
{
    public Task<Flashcard?> Handle(GetFlashcardQuery query)
    {
        return _flashcardRepository.GetByIdAsync(query.Id);
    }
}
