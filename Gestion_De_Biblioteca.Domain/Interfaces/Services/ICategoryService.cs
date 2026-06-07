using Gestion_De_Biblioteca.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category?> UpdateCategoryAsync(int id, Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
