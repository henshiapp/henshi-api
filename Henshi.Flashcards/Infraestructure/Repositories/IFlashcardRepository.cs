using System;
using Henshi.Flashcards.Domain.Models;
using Henshi.Shared.Infraestructure.Repositories;

namespace Henshi.Flashcards.Infraestructure.Repositories;

public interface IFlashcardRepository : IBaseRepository<Flashcard>
{
    Task<List<Flashcard>> ListAllAvailableForRecallAsync(Guid collectionId);
    Task<List<Flashcard>> ListByCollectionId(Guid collectionId);
}
