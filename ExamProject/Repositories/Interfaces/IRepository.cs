// Repositories/IRepository.cs
using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
    // Index - Get all with optional includes
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetAllWithNestedIncludesAsync(params string[] includePaths);
    Task<IEnumerable<T>> GetAllWithNestedIncludesAndFilterAsync
        (
            Expression<Func<T, bool>>? filter = null,
            params string[] includePaths
        );

    // Details - Get single by ID with optional includes
    Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<T> GetByIdWithNestedIncludesAsync(int id, params string[] includePaths);


    // Create
    Task AddAsync(T entity);

    // Update
    void Update(T entity);
    Task UpdateAsync(T entity);

    // Delete
    Task DeleteAsync(int id);
    Task DeleteWithNestedIncludesAsync(int id, params string[] includePaths);

    // Check if exists
    Task<bool> ExistsAsync(int id);
}