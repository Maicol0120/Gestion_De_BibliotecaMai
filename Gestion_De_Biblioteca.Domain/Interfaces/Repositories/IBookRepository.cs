using Gestion_De_Biblioteca.Domain.Entities;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<IReadOnlyList<Book>> GetAllWithDetailsAsync();
    Task<Book?> GetByIdWithDetailsAsync(int id);
    Task<IReadOnlyList<Book>> SearchByCategoryAsync(string? category);
}
