using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class BookRepository(LibraryDbContext context) : GenericRepository<Book>(context), IBookRepository
{
    private new readonly LibraryDbContext _context = context;

    public async Task<IEnumerable<Book>> GetAllWithDetailsAsync() =>
        await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .ToListAsync();

    public Task<Book?> GetByIdWithDetailsAsync(int id) =>
        _context.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<IReadOnlyList<Book>> SearchByCategoryAsync(string? category)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToListAsync();
        }

        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .Where(b => b.Category!.Name.Contains(category))
            .ToListAsync();
    }
}