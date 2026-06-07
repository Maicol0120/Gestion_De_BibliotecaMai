using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Repositories;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<IEnumerable<Book>> GetAllWithDetailsAsync();
    Task<Book?> GetByIdWithDetailsAsync(int id);
    Task<IReadOnlyList<Book>> SearchByCategoryAsync(string? category);
}