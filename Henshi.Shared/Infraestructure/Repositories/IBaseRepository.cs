namespace Henshi.Shared.Infraestructure.Repositories;

public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(Guid id, string userId);
    Task AddAsync(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}
