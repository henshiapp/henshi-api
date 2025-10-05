using Henshi.Flashcards.FlashcardCollections.Models;
using Henshi.Flashcards.FlashcardCollections.Repositories;
using Henshi.Shared.Dtos;

namespace Henshi.Flashcards.FlashcardCollections.Features.ListFlashcardCollections.V1;

class ListFlashcardCollectionsQueryHandler(IFlashcardCollectionRepository _flashcardCollectionRepository)
{
    public async Task<(List<FlashcardCollection>, PaginationMetadata)> Handle(ListFlashcardCollectionsQuery query)
    {
        return await _flashcardCollectionRepository.ListAsync(
            query.Search,
            query.Page,
            query.PageSize
        );
    }
}