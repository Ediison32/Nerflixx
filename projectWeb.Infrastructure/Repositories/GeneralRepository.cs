using DefaultNamespace;
using Microsoft.EntityFrameworkCore;
using projectWeb.Infrastructure.Data;

namespace projectWeb.Infrastructure.Repositories;

public class GeneralRepository<T> :  IGeneral<T> where T : class
{
    // This is a general crud for the all tables in de db 
    protected readonly AppDbContext _context;
    
    // Since it is a generic repostory , what Dbset does is take or transform itself into any table that is requeted,  user, movies etc 
    protected readonly DbSet<T> _dbSet;  

    // nota imporante se pode " portected " para que los hijos que hereden este generalRespositorie pueda usar los metodos que estan aqui 
    // Important note: This repository can be "protected" so that children who inherit it can use the methods described here.
    public GeneralRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();

    }


    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}