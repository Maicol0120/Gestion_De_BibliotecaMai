using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class AuthorRepository(LibraryDbContext context) : GenericRepository<Author>(context), IAuthorRepository
{
    private new readonly LibraryDbContext _context = context;

    public Task<bool> ExistsByNameAsync(string firstName, string lastName) =>
        _context.Authors.AnyAsync(author => author.FirstName == firstName && author.LastName == lastName);

    public Task<bool> ExistsByNameExcludingIdAsync(string firstName, string lastName, int excludeId) =>
        _context.Authors.AnyAsync(author => author.Id != excludeId && author.FirstName == firstName && author.LastName == lastName);

    public async Task<IEnumerable<Author>> GetWithBooksAsync() =>
        await _context.Authors
            .AsNoTracking()
            .Include(author => author.Books)
            .OrderBy(author => author.LastName)
            .ToListAsync();



  public new async Task SaveChangesAsync()
    {
      
        await _context.SaveChangesAsync();
    }

    public new void Update(Author existingAuthor)
    {
        
        _context.Authors.Update(existingAuthor);
    }
}
