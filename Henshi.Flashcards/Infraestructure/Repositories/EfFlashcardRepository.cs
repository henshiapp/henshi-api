using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Infraestructure.Database;
using Henshi.Shared.Infraestructure.Repositories;
using Henshi.Shared.Presentation.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Flashcards.Infraestructure.Repositories;

public class EfFlashcardRepository : BaseRepository<Flashcard>, IFlashcardRepository
{
    private readonly FlashcardDbContext _dbContext;

    public EfFlashcardRepository(FlashcardDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<Flashcard?> GetByIdAsync(Guid id, string userId)
    {
        return await _dbContext.Flashcards
            .Where(f => f.Id == id && f.Collection.UserId == userId)
            .SingleOrDefaultAsync();
    }

    public async Task<(List<Flashcard>, PaginationMetadata)> ListAsync(Guid collectionId, string? search, string userId, int page, int pageSize)
    {
        var baseQuery = _dbContext.Flashcards.AsQueryable()
            .Where(f => f.CollectionId == collectionId && f.Collection.UserId == userId);

        var offset = (page - 1) * pageSize;
        var limit = pageSize;

        if (search is not null)
        {
            baseQuery = baseQuery.Where(f => search.Contains(f.Answer) || search.Contains(f.Question));
        }

        var paginatedQuery = baseQuery.Skip(offset).Take(limit);
        var elementsCount = await baseQuery.CountAsync();

        return (await paginatedQuery.ToListAsync(), new PaginationMetadata
        {
            Page = page,
            Offset = offset,
            Size = pageSize,
            TotalElements = elementsCount,
            TotalPages = elementsCount / pageSize
        });
    }

    public async Task<List<Flashcard>> ListAllAvailableForRecallAsync(Guid collectionId, string userId)
    {
        return await _dbContext.Flashcards.AsQueryable()
            .Where(f => f.CollectionId == collectionId && f.Collection.UserId == userId && f.NextRecall <= DateTime.UtcNow)
            .ToListAsync();
    }

    public Task<List<Flashcard>> ListByCollectionId(Guid collectionId, string userId)
    {
        return _dbContext.Flashcards.AsQueryable()
            .Where(f => f.CollectionId == collectionId && f.Collection.UserId == userId)
            .ToListAsync();
    }
}