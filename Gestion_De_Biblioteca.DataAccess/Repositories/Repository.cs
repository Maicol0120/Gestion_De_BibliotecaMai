using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class Repository<T>(LibraryDbContext context) : IRepository<T> where T : class
{
    protected readonly LibraryDbContext Context = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public async Task<IReadOnlyList<T>> GetAllAsync() =>
        await DbSet.AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(int id) =>
        await DbSet.FindAsync(id);

    public async Task AddAsync(T entity) =>
        await DbSet.AddAsync(entity);

    public void Update(T entity) =>
        DbSet.Update(entity);

    public void Delete(T entity) =>
        DbSet.Remove(entity);

    public async Task<bool> ExistsAsync(int id) =>
        await DbSet.FindAsync(id) is not null;

    public async Task SaveChangesAsync() =>
        await Context.SaveChangesAsync();
}
