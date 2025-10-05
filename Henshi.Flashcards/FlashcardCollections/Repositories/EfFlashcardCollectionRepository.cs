using Henshi.Flashcards.Database;
using Henshi.Flashcards.FlashcardCollections.Models;
using Henshi.Shared.Dtos;
using Henshi.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Flashcards.FlashcardCollections.Repositories;

public class EfFlashcardCollectionRepository : BaseRepository<FlashcardCollection>, IFlashcardCollectionRepository
{
    private readonly FlashcardDbContext _dbContext;

    public EfFlashcardCollectionRepository(FlashcardDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<FlashcardCollection?> GetByIdAsync(Guid id)
    {
        return await _dbContext.FlashcardCollections
            .Include(fc => fc.Flashcards)
            .Where(fc => fc.Id == id)
            .SingleOrDefaultAsync();
    }

    public async Task<long> GetCount()
    {
        return await _dbContext.FlashcardCollections.AsNoTracking()
            .CountAsync();
    }

    public async Task<(List<FlashcardCollection>, PaginationMetadata)> ListAsync(string? search, int page, int pageSize)
    {
        var baseQuery = _dbContext.FlashcardCollections.AsQueryable().AsNoTracking();

        var offset = (page - 1) * pageSize;
        var limit = pageSize;

        if (search is not null)
        {
            var searchLowered = search.ToLower();
            baseQuery = baseQuery.Where(c => c.Title.ToLower().Contains(searchLowered)
                || (c.Description != null && c.Description.ToLower().Contains(searchLowered)));
        }

        var paginatedQuery = baseQuery.OrderBy(c => c.CreatedAt).Skip(offset).Take(limit);
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
}