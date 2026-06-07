using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Repositories;

public interface ILoanRepository : IGenericRepository<Loan>
{
    Task<IReadOnlyList<Loan>> GetHistoryAsync();
    Task<IReadOnlyList<Loan>> GetHistoryByMemberAsync(int memberId);
    Task<Loan?> GetByIdWithDetailsAsync(int id);
}