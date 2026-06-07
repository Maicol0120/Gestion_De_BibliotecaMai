using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class AuthorRepository(LibraryDbContext context) : GenericRepository<Author>(context), IAuthorRepository
{
    public async Task<bool> ExistsByNameAsync(string firstName, string lastName) =>
        await Context.Authors.AnyAsync(author => author.FirstName == firstName && author.LastName == lastName);

    public async Task<bool> ExistsByNameExcludingIdAsync(string firstName, string lastName, int excludeId) =>
        await Context.Authors.AnyAsync(author =>
            author.Id != excludeId && author.FirstName == firstName && author.LastName == lastName);

    public async Task<IEnumerable<Author>> GetWithBooksAsync() =>
        await Context.Authors
            .AsNoTracking()
            .Include(author => author.Books)
            .OrderBy(author => author.LastName)
            .ToListAsync();
}
