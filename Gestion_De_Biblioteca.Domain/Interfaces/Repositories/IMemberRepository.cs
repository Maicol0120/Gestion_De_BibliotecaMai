using Gestion_De_Biblioteca.Domain.Entities;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

public interface IMemberRepository : IGenericRepository<Member>
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByEmailExcludingIdAsync(string email, int excludeId);
}
