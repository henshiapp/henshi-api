using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Infraestructure.Database;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Shared.Presentation.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Flashcards.Infraestructure.Repositories;

public class EfFlashcardRepository : IFlashcardRepository
{
    private readonly FlashcardDbContext _dbContext;

    public EfFlashcardRepository(FlashcardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(Flashcard entity)
    {
        _dbContext.AddAsync(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Flashcard entity)
    {
        _dbContext.Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _dbContext.Remove(id);
        return Task.CompletedTask;
    }

    public async Task<Flashcard?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Flashcards.FindAsync(id);
    }

    public async Task<List<Flashcard>> ListAllAvailableForRecallAsync(Guid collectionId)
    {
        return await _dbContext.Flashcards.AsQueryable()
            .Where(f => f.CollectionId == collectionId && f.NextRecall <= DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<(List<Flashcard>, PaginationMetadata)> ListAsync(string? search, int page, int pageSize)
    {
        var baseQuery = _dbContext.Flashcards.AsQueryable();

        var offset = page * pageSize;
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

    public Task<List<Flashcard>> ListByCollectionId(Guid collectionId)
    {
        return _dbContext.Flashcards.AsQueryable()
            .Where(f => f.CollectionId == collectionId)
            .ToListAsync();
    }

    public Task SaveChangesAsync()
    {
        _dbContext.SaveChangesAsync();
        return Task.CompletedTask;
    }
}