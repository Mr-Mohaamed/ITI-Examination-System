using ExamProject.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }

    public class BaseRepository<T>(AppDbContext context) : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context = context;

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
        public async Task DeleteAsync(T entity) => await Task.Run(() => _context.Set<T>().Remove(entity));
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
        public async Task UpdateAsync(T entity) => await Task.Run(() => _context.Set<T>().Update(entity));
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
