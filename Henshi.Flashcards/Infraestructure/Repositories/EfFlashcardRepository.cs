using Henshi.Flashcards.Domain;
using Henshi.Flashcards.Infraestructure;
using Henshi.Flashcards.Infraestructure.Database;
using Henshi.Flashcards.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Flashcards.Application.Extensions;

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

    public async Task<Flashcard?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Flashcards.FindAsync(id);
    }

    public async Task<List<Flashcard>> ListAsync()
    {
        return await _dbContext.Flashcards.ToListAsync();
    }

    public Task SaveChangesAsync()
    {
        _dbContext.SaveChangesAsync();
        return Task.CompletedTask;
    }
}