using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Infraestructure;
using Henshi.Flashcards.Infraestructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Flashcards.Application.Extensions;

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

    public async Task<FlashcardCollection?> GetByIdAsync(Guid id)
    {
        return await _dbContext.FlashcardCollections.FindAsync(id);
    }

    public async Task<List<FlashcardCollection>> ListAsync()
    {
        return await _dbContext.FlashcardCollections.ToListAsync();
    }

    public Task SaveChangesAsync()
    {
        _dbContext.SaveChangesAsync();
        return Task.CompletedTask;
    }
}