using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Flashcards.Models;


namespace Henshi.Flashcards.Flashcards.Features.GetFlashcard.V1;

class GetRecallQueryHandler(IFlashcardRepository _flashcardRepository)
{
    public async Task<List<Flashcard>> Handle(GetRecallQuery query)
    {
        return await _flashcardRepository.ListAllAvailableForRecallAsync(query.CollectionId);
    }
}