// Repositories/Repository.cs
using ExamProject.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }
    public async Task<IEnumerable<T>> GetAllWithNestedIncludesAsync(params string[] includePaths)
    {
        IQueryable<T> query = _dbSet;

        foreach (var includePath in includePaths)
        {
            query = query.Include(includePath);
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        //return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        return await query.FirstAsync(e => EF.Property<int>(e, "Id") == id);
    }

    // New method to handle nested includes for GetById
    public async Task<T> GetByIdWithNestedIncludesAsync(int id, params string[] includePaths)
    {
        IQueryable<T> query = _dbSet;

        foreach (var includePath in includePaths)
        {
            query = query.Include(includePath);
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        Console.WriteLine("Before Update");
        try
        {
            _dbSet.Update(entity);
            Console.WriteLine("After Update, before Save");
            _context.SaveChanges();
            Console.WriteLine("After Save");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex);
            throw;
        }
    }
    public async Task UpdateAsync(T entity)
    {
        Console.WriteLine("Before Update");
        try
        {
            _dbSet.Update(entity);
            Console.WriteLine("After Update, before Save");
            await _context.SaveChangesAsync();
            Console.WriteLine("After Save");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex);
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id);
    }
}