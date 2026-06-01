using Gestion_De_Biblioteca.Domain.Entities;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

public interface ILoanRepository : IRepository<Loan>
{
    Task<IReadOnlyList<Loan>> GetHistoryAsync();
    Task<IReadOnlyList<Loan>> GetHistoryByMemberAsync(int memberId);
    Task<Loan?> GetByIdWithDetailsAsync(int id);
}
