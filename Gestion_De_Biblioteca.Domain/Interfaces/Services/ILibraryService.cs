using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Services;

namespace Gestion_De_Biblioteca.Domain.Interfaces.Services;

public interface ILibraryService
{
    Task<IReadOnlyList<Author>> GetAuthorsAsync();
    Task<Author?> GetAuthorAsync(int id);
    Task<Author> CreateAuthorAsync(Author author);
    Task<Author?> UpdateAuthorAsync(int id, Author author);
    Task<bool> DeleteAuthorAsync(int id);

    Task<IReadOnlyList<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryAsync(int id);
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category?> UpdateCategoryAsync(int id, Category category);
    Task<bool> DeleteCategoryAsync(int id);

    Task<IReadOnlyList<Book>> GetBooksAsync();
    Task<Book?> GetBookAsync(int id);
    Task<IReadOnlyList<Book>> SearchBooksByCategoryAsync(string? category);
    Task<ServiceResult<Book>> CreateBookAsync(Book book);
    Task<ServiceResult<Book>> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id);

    Task<IReadOnlyList<Member>> GetMembersAsync();
    Task<Member?> GetMemberAsync(int id);
    Task<Member> CreateMemberAsync(Member member);
    Task<Member?> UpdateMemberAsync(int id, Member member);
    Task<bool> DeleteMemberAsync(int id);

    Task<IReadOnlyList<Loan>> GetLoanHistoryAsync();
    Task<IReadOnlyList<Loan>> GetLoanHistoryByMemberAsync(int memberId);
    Task<ServiceResult<Loan>> CreateLoanAsync(int bookId, int memberId, DateOnly dueDate);
    Task<ServiceResult<Loan>> ReturnLoanAsync(int id, DateOnly? returnDate);
}
