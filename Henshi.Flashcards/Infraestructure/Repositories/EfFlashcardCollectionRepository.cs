using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Infraestructure.Database;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Shared.Infraestructure.Repositories;
using Henshi.Shared.Presentation.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Flashcards.Infraestructure.Repositories;

public class EfFlashcardCollectionRepository : BaseRepository<FlashcardCollection>, IFlashcardCollectionRepository
{
    private readonly FlashcardDbContext _dbContext;

    public EfFlashcardCollectionRepository(FlashcardDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<FlashcardCollection?> GetByIdAsync(Guid id, string userId)
    {
        return await _dbContext.FlashcardCollections
            .Include(fc => fc.Flashcards)
            .Where(fc => fc.Id == id && fc.UserId == userId)
            .SingleOrDefaultAsync();
    }

    public async Task<long> GetCount(string userId)
    {
        return await _dbContext.FlashcardCollections.AsNoTracking()
            .Where(c => c.UserId == userId)
            .CountAsync();
    }

    public async Task<(List<FlashcardCollection>, PaginationMetadata)> ListAsync(string? search, string userId, int page, int pageSize)
    {
        var baseQuery = _dbContext.FlashcardCollections.AsQueryable().AsNoTracking()
            .Where(c => c.UserId == userId);

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