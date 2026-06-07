using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class GenericRepository<T>(LibraryDbContext context) : IGenericRepository<T> where T : class
{
    protected readonly LibraryDbContext Context = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await DbSet.AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(int id) =>
        await DbSet.FindAsync(id);

    public async Task<T> CreateAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(int id, T entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null)
        {
            return;
        }

        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id) =>
        await DbSet.FindAsync(id) is not null;
}
