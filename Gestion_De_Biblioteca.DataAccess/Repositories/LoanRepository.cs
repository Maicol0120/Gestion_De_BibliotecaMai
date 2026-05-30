using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class LoanRepository(LibraryDbContext context) : Repository<Loan>(context), ILoanRepository
{
    public async Task<IReadOnlyList<Loan>> GetHistoryAsync() =>
        await Context.Loans
            .AsNoTracking()
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .OrderByDescending(loan => loan.LoanDate)
            .ToListAsync();

    public async Task<IReadOnlyList<Loan>> GetHistoryByMemberAsync(int memberId) =>
        await Context.Loans
            .AsNoTracking()
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .Where(loan => loan.MemberId == memberId)
            .OrderByDescending(loan => loan.LoanDate)
            .ToListAsync();

    public async Task<Loan?> GetByIdWithDetailsAsync(int id) =>
        await Context.Loans
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .FirstOrDefaultAsync(loan => loan.Id == id);
}
