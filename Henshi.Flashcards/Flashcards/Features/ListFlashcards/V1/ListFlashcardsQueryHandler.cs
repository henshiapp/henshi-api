using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Flashcards.Models;
using Henshi.Shared.Dtos;

namespace Henshi.Flashcards.Flashcards.Features.ListFlashcards.V1;

class ListFlashcardsQueryHandler(IFlashcardRepository _flashcardRepository)
{
    public async Task<(List<Flashcard>, PaginationMetadata)> Handle(ListFlashcardsQuery query)
    {
        return await _flashcardRepository.ListAsync(
            query.CollectionId,
            query.Search,
            query.Page,
            query.PageSize
        );
    }
}