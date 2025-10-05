using Henshi.Flashcards.FlashcardCollections.Models;
using Henshi.Flashcards.FlashcardCollections.Repositories;


namespace Henshi.Flashcards.FlashcardCollections.Features.GetFlashcardCollection.V1;

class GetFlashcardCollectionQueryHandler(IFlashcardCollectionRepository _flashcardCollectionRepository)
{
    public async Task<FlashcardCollection?> Handle(GetFlashcardCollectionQuery query)
    {
        return await _flashcardCollectionRepository.GetByIdAsync(query.Id);
    }
}
