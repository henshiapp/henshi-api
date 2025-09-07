using System;
using Henshi.Flashcards.Domain.Models;
using Henshi.Shared.Infraestructure.Repositories;
using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Flashcards.Domain.Repositories;

public interface IFlashcardRepository : IBaseRepository<Flashcard>
{
    Task<(List<Flashcard>, PaginationMetadata)> ListAsync(Guid collectionId, string? search, string userId, int page, int pageSize);
    Task<List<Flashcard>> ListAllAvailableForRecallAsync(Guid collectionId, string userId);
    Task<List<Flashcard>> ListByCollectionId(Guid collectionId, string userId);
}
