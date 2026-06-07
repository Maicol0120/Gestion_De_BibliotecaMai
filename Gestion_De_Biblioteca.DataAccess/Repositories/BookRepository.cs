using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class BookRepository(LibraryDbContext context) : GenericRepository<Book>(context), IBookRepository
{
    public async Task<bool> ExistsByIsbnAsync(string isbn) =>
        await Context.Books.AnyAsync(book => book.Isbn == isbn);

    public async Task<bool> ExistsByIsbnExcludingIdAsync(string isbn, int excludeId) =>
        await Context.Books.AnyAsync(book => book.Id != excludeId && book.Isbn == isbn);

    public async Task<IEnumerable<Book>> GetByCategoryAsync(int categoryId) =>
        await Context.Books
            .AsNoTracking()
            .Include(book => book.Author)
            .Include(book => book.Category)
            .Where(book => book.CategoryId == categoryId)
            .OrderBy(book => book.Title)
            .ToListAsync();

    public async Task<IEnumerable<Book>> GetByAuthorAsync(int authorId) =>
        await Context.Books
            .AsNoTracking()
            .Include(book => book.Author)
            .Include(book => book.Category)
            .Where(book => book.AuthorId == authorId)
            .OrderBy(book => book.Title)
            .ToListAsync();

    public async Task<IEnumerable<Book>> GetAllWithDetailsAsync() =>
        await Context.Books
            .AsNoTracking()
            .Include(book => book.Author)
            .Include(book => book.Category)
            .OrderBy(book => book.Title)
            .ToListAsync();

    public async Task<Book?> GetWithDetailsAsync(int id) =>
        await Context.Books
            .Include(book => book.Author)
            .Include(book => book.Category)
            .FirstOrDefaultAsync(book => book.Id == id);

    public async Task<IEnumerable<Book>> GetByCategoryNameAsync(string? category)
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
