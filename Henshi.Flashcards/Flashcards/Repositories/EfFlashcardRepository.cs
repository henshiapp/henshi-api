using Henshi.Flashcards.Database;
using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Flashcards.Models;
using Henshi.Shared.Dtos;
using Henshi.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Flashcards.Infraestructure.Repositories;

public class EfFlashcardRepository : BaseRepository<Flashcard>, IFlashcardRepository
{
    private readonly FlashcardDbContext _dbContext;

    public EfFlashcardRepository(FlashcardDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<Flashcard?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Flashcards
            .Where(f => f.Id == id)
            .SingleOrDefaultAsync();
    }

    public async Task<(List<Flashcard>, PaginationMetadata)> ListAsync(Guid collectionId, string? search, int page, int pageSize)
    {
        var baseQuery = _dbContext.Flashcards.AsQueryable().AsNoTracking()
            .Where(f => f.CollectionId == collectionId);

        var offset = (page - 1) * pageSize;
        var limit = pageSize;

        if (search is not null)
        {
            var searchLowered = search.ToLower();
            baseQuery = baseQuery.Where(f => EF.Functions.Like(f.Question.ToLower(), $"%{searchLowered}%")
                || EF.Functions.Like(f.Answer.ToLower(), $"%{searchLowered}%"));
        }

        var paginatedQuery = baseQuery.OrderBy(f => f.CreatedAt).Skip(offset).Take(limit);
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

    public async Task<List<Flashcard>> ListAllAvailableForRecallAsync(Guid collectionId)
    {
        return await _dbContext.Flashcards.AsQueryable()
            .Where(f => f.CollectionId == collectionId && f.NextRecall <= DateTime.UtcNow)
            .ToListAsync();
    }

    public Task<List<Flashcard>> ListByCollectionId(Guid collectionId)
    {
        return _dbContext.Flashcards.AsQueryable()
            .Where(f => f.CollectionId == collectionId)
            .ToListAsync();
    }

    public async Task<long> GetCount()
    {
        return await _dbContext.Flashcards.AsNoTracking()
            .CountAsync();
    }
}