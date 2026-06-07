using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Repositories;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    new Task AddAsync(Category category);
    new void Delete(Category category);
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsByNameExcludingIdAsync(string name, int excludeId);
    new Task SaveChangesAsync();
    new void Update(Category existingCategory);
}
