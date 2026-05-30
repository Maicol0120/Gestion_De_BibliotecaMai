using Gestion_De_Biblioteca.API.Dtos;

namespace Gestion_De_Biblioteca.API.Services;

public interface ILibraryService
{
    Task<IReadOnlyList<AuthorResponse>> GetAuthorsAsync();
    Task<AuthorResponse?> GetAuthorAsync(int id);
    Task<AuthorResponse> CreateAuthorAsync(AuthorRequest request);
    Task<AuthorResponse?> UpdateAuthorAsync(int id, AuthorRequest request);
    Task<bool> DeleteAuthorAsync(int id);

    Task<IReadOnlyList<CategoryResponse>> GetCategoriesAsync();
    Task<CategoryResponse?> GetCategoryAsync(int id);
    Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request);
    Task<CategoryResponse?> UpdateCategoryAsync(int id, CategoryRequest request);
    Task<bool> DeleteCategoryAsync(int id);

    Task<IReadOnlyList<BookResponse>> GetBooksAsync();
    Task<BookResponse?> GetBookAsync(int id);
    Task<IReadOnlyList<BookResponse>> SearchBooksByCategoryAsync(string? category);
    Task<ServiceResult<BookResponse>> CreateBookAsync(BookRequest request);
    Task<ServiceResult<BookResponse>> UpdateBookAsync(int id, BookRequest request);
    Task<bool> DeleteBookAsync(int id);

    Task<IReadOnlyList<MemberResponse>> GetMembersAsync();
    Task<MemberResponse?> GetMemberAsync(int id);
    Task<MemberResponse> CreateMemberAsync(MemberRequest request);
    Task<MemberResponse?> UpdateMemberAsync(int id, MemberRequest request);
    Task<bool> DeleteMemberAsync(int id);

    Task<IReadOnlyList<LoanResponse>> GetLoanHistoryAsync();
    Task<IReadOnlyList<LoanResponse>> GetLoanHistoryByMemberAsync(int memberId);
    Task<ServiceResult<LoanResponse>> CreateLoanAsync(LoanRequest request);
    Task<ServiceResult<LoanResponse>> ReturnLoanAsync(int id, ReturnLoanRequest request);
}
