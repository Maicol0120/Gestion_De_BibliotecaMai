using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class CategoryRepository(LibraryDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    public async Task<bool> ExistsByNameAsync(string name) =>
        await Context.Categories.AnyAsync(category => category.Name == name);

    public async Task<bool> ExistsByNameExcludingIdAsync(string name, int excludeId) =>
        await Context.Categories.AnyAsync(category => category.Id != excludeId && category.Name == name);
}
