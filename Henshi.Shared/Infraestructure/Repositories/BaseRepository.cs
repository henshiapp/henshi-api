using System;
using Henshi.Shared.Presentation.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Henshi.Shared.Infraestructure.Repositories;

public abstract class BaseRepository<T>(DbContext dbContext) : IBaseRepository<T> where T : notnull
{
    private readonly DbContext _dbContext = dbContext;

    public async Task AddAsync(T entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Remove(entity);
    }

    public virtual Task<T?> GetByIdAsync(Guid id, string userId)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
