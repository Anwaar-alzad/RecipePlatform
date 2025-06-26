using Microsoft.EntityFrameworkCore;
using RecipePlatform.BLL.Interfaces.Repositories;
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
        _context.SaveChanges();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }
}
