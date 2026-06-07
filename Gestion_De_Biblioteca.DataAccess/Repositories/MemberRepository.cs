using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class MemberRepository(LibraryDbContext context) : GenericRepository<Member>(context), IMemberRepository
{
    public async Task<bool> ExistsByEmailAsync(string email) =>
        await Context.Members.AnyAsync(member => member.Email == email);

    public async Task<bool> ExistsByEmailExcludingIdAsync(string email, int excludeId) =>
        await Context.Members.AnyAsync(member => member.Id != excludeId && member.Email == email);
}
