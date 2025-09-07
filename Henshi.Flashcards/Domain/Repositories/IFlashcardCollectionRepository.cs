using Henshi.Flashcards.Domain.Models;
using Henshi.Shared.Infraestructure.Repositories;
using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Flashcards.Domain.Repositories;

public interface IFlashcardCollectionRepository : IBaseRepository<FlashcardCollection>
{
    public Task<(List<FlashcardCollection>, PaginationMetadata)> ListAsync(string? search, string userId, int page, int pageSize);
}
