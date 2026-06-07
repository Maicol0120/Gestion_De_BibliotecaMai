using Gestion_De_Biblioteca.Domain.Entities;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Services;

public interface IAuthorService
{
    Task<IReadOnlyList<Author>> GetAuthorsAsync();
    Task<Author?> GetAuthorAsync(int id);
    Task<Author> CreateAuthorAsync(Author author);
    Task<Author?> UpdateAuthorAsync(int id, Author author);
    Task<bool> DeleteAuthorAsync(int id);
}



