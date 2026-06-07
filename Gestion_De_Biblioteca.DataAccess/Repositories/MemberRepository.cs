using Gestion_De_Biblioteca.DataAccess.Data;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gestion_De_Biblioteca.DataAccess.Repositories;

public class MemberRepository(LibraryDbContext context) : GenericRepository<Member>(context), IMemberRepository
{
    private new readonly LibraryDbContext _context = context;

    public Task<bool> ExistsByEmailAsync(string email) =>
        _context.Members.AnyAsync(member => member.Email == email);

    public Task<bool> ExistsByEmailExcludingIdAsync(string email, int excludeId) =>
        _context.Members.AnyAsync(member => member.Id != excludeId && member.Email == email);
}
