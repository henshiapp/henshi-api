using Henshi.Flashcards.Flashcards.Models;
using Henshi.Shared.Dtos;
using Henshi.Shared.Repositories;

namespace Henshi.Flashcards.Domain.Repositories;

public interface IFlashcardRepository : IBaseRepository<Flashcard>
{
    Task<(List<Flashcard>, PaginationMetadata)> ListAsync(Guid collectionId, string? search, int page, int pageSize);
    Task<List<Flashcard>> ListAllAvailableForRecallAsync(Guid collectionId);
    Task<List<Flashcard>> ListByCollectionId(Guid collectionId);
    Task<long> GetCount();
}
