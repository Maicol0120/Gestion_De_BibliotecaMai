using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class BookRepository(LibraryDbContext context) : Repository<Book>(context), IBookRepository
{
    public async Task<IReadOnlyList<Book>> GetAllWithDetailsAsync() =>
        await Context.Books
            .AsNoTracking()
            .Include(book => book.Author)
            .Include(book => book.Category)
            .OrderBy(book => book.Title)
            .ToListAsync();

    public async Task<Book?> GetByIdWithDetailsAsync(int id) =>
        await Context.Books
            .Include(book => book.Author)
            .Include(book => book.Category)
            .FirstOrDefaultAsync(book => book.Id == id);

    public async Task<IReadOnlyList<Book>> SearchByCategoryAsync(string? category)
    {
        var query = Context.Books
            .AsNoTracking()
            .Include(book => book.Author)
            .Include(book => book.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(book => book.Category != null && book.Category.Name.Contains(category));
        }

        return await query
            .OrderBy(book => book.Title)
            .ToListAsync();
    }
}
