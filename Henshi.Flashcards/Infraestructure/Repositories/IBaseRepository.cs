namespace Henshi.Flashcards.Infraestructure.Repositories;

public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> ListAsync();
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveChangesAsync();
}
