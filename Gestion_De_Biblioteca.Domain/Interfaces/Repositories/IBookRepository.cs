using Gestion_De_Biblioteca.Domain.Entities;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<bool> ExistsByIsbnAsync(string isbn);
    Task<bool> ExistsByIsbnExcludingIdAsync(string isbn, int excludeId);
    Task<IEnumerable<Book>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Book>> GetByCategoryNameAsync(string? category);
    Task<IEnumerable<Book>> GetByAuthorAsync(int authorId);
    Task<Book?> GetWithDetailsAsync(int id);
    Task<IEnumerable<Book>> GetAllWithDetailsAsync();
}
