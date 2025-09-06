using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Shared.Infraestructure.Repositories;

public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<(List<T>, PaginationMetadata)> ListAsync(string userId, string? search, int page, int pageSize);
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}
