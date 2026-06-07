using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Repositories;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Repositories;

public interface IMemberRepository : IGenericRepository<Member>
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByEmailExcludingIdAsync(string email, int excludeId);
}