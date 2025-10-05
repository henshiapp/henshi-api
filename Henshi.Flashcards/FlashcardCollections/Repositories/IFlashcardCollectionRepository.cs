using Henshi.Flashcards.FlashcardCollections.Models;
using Henshi.Shared.Dtos;
using Henshi.Shared.Repositories;

namespace Henshi.Flashcards.FlashcardCollections.Repositories;

public interface IFlashcardCollectionRepository : IBaseRepository<FlashcardCollection>
{
    public Task<(List<FlashcardCollection>, PaginationMetadata)> ListAsync(string? search, int page, int pageSize);
    Task<long> GetCount();
}
