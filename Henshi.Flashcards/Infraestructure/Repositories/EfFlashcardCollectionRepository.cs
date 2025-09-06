using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Infraestructure.Database;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Shared.Presentation.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Flashcards.Infraestructure.Repositories;

public class EfFlashcardCollectionRepository : IFlashcardCollectionRepository
{
    private readonly FlashcardDbContext _dbContext;

    public EfFlashcardCollectionRepository(FlashcardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(FlashcardCollection entity)
    {
        _dbContext.AddAsync(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(FlashcardCollection entity)
    {
        _dbContext.Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _dbContext.Remove(id);
        return Task.CompletedTask;
    }

    public async Task<FlashcardCollection?> GetByIdAsync(Guid id)
    {
        return await _dbContext.FlashcardCollections.FindAsync(id);
    }

    public async Task<List<FlashcardCollection>> ListAsync()
    {
        return await _dbContext.FlashcardCollections.ToListAsync();
    }

    public async Task<(List<FlashcardCollection>, PaginationMetadata)> ListAsync(string userId, string? search, int page, int pageSize)
    {
        var baseQuery = _dbContext.FlashcardCollections.AsQueryable().Where(c => c.UserId == userId);
        
        var offset = (page - 1) * pageSize;
        var limit = pageSize;

        if (search is not null)
        {
            baseQuery = baseQuery.Where(c => search.Contains(c.Title) || (c.Description != null && search.Contains(c.Description)));
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

    public Task SaveChangesAsync()
    {
        _dbContext.SaveChangesAsync();
        return Task.CompletedTask;
    }
}