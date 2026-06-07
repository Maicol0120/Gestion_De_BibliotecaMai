using Gestion_De_Biblioteca.Domain.Entities;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task AddAsync(Category category);
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsByNameExcludingIdAsync(string name, int excludeId);
    Task SaveChangesAsync();
    void Update(Category existingCategory);
}
