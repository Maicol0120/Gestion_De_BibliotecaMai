using Gestion_De_Biblioteca.Domain.Entities;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

public interface ILoanRepository : IGenericRepository<Loan>
{
    Task<IEnumerable<Loan>> GetByMemberIdAsync(int memberId);
    Task<IEnumerable<Loan>> GetByBookIdAsync(int bookId);
    Task<IEnumerable<Loan>> GetActiveLoansAsync();
    Task<IEnumerable<Loan>> GetOverdueLoansAsync();
    Task<Loan?> GetWithDetailsAsync(int id);
    Task<bool> HasActiveLoanAsync(int memberId, int bookId);
    Task SaveChangesAsync();
}
