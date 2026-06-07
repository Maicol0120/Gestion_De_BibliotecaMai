using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class LoanRepository(LibraryDbContext context) : GenericRepository<Loan>(context), ILoanRepository
{
    public async Task<IEnumerable<Loan>> GetAllAsync() =>
        await Context.Loans
            .AsNoTracking()
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .OrderByDescending(loan => loan.LoanDate)
            .ToListAsync();

    public async Task<IEnumerable<Loan>> GetByMemberIdAsync(int memberId) =>
        await Context.Loans
            .AsNoTracking()
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .Where(loan => loan.MemberId == memberId)
            .OrderByDescending(loan => loan.LoanDate)
            .ToListAsync();

    public async Task<IEnumerable<Loan>> GetByBookIdAsync(int bookId) =>
        await Context.Loans
            .AsNoTracking()
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .Where(loan => loan.BookId == bookId)
            .OrderByDescending(loan => loan.LoanDate)
            .ToListAsync();

    public async Task<IEnumerable<Loan>> GetActiveLoansAsync() =>
        await Context.Loans
            .AsNoTracking()
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .Where(loan => !loan.ReturnDate.HasValue)
            .OrderByDescending(loan => loan.LoanDate)
            .ToListAsync();

    public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        return await Context.Loans
            .AsNoTracking()
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .Where(loan => !loan.ReturnDate.HasValue && loan.DueDate < today)
            .OrderByDescending(loan => loan.LoanDate)
            .ToListAsync();
    }

    public async Task<Loan?> GetWithDetailsAsync(int id) =>
        await Context.Loans
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .FirstOrDefaultAsync(loan => loan.Id == id);

    public async Task<bool> HasActiveLoanAsync(int memberId, int bookId) =>
        await Context.Loans.AnyAsync(loan =>
            loan.MemberId == memberId && loan.BookId == bookId && !loan.ReturnDate.HasValue);
}
