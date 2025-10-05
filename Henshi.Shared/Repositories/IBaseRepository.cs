namespace Henshi.Shared.Repositories;

public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}
