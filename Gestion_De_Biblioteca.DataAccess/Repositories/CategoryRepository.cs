using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class CategoryRepository(LibraryDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    private new readonly LibraryDbContext _context = context;

    public Task<bool> ExistsByNameAsync(string name) =>
        _context.Categories.AnyAsync(category => category.Name == name);

    public Task<bool> ExistsByNameExcludingIdAsync(string name, int excludeId) =>
        _context.Categories.AnyAsync(category => category.Id != excludeId && category.Name == name);
}
