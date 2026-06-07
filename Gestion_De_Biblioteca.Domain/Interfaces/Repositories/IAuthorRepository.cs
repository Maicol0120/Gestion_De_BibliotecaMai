
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Repositories;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

public interface IAuthorRepository : IGenericRepository<Author>
{
    Task<bool> ExistsByNameAsync(string firstName, string lastName);
    Task<bool> ExistsByNameExcludingIdAsync(string firstName, string lastName, int excludeId);
    Task<IEnumerable<Author>> GetWithBooksAsync();
}