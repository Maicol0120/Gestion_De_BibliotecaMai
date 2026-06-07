using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class LoanRepository(LibraryDbContext context) : GenericRepository<Loan>(context), ILoanRepository
{
    private new readonly LibraryDbContext _context = context;

    public async Task<IReadOnlyList<Loan>> GetHistoryAsync() =>
        await _context.Loans
            .AsNoTracking()
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .OrderByDescending(loan => loan.LoanDate)
            .ToListAsync();

    public async Task<IReadOnlyList<Loan>> GetHistoryByMemberAsync(int memberId) =>
        await _context.Loans
            .AsNoTracking()
            .Where(loan => loan.MemberId == memberId)
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .OrderByDescending(loan => loan.LoanDate)
            .ToListAsync();

    public Task<Loan?> GetByIdWithDetailsAsync(int id) =>
        _context.Loans
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .FirstOrDefaultAsync(loan => loan.Id == id);
}