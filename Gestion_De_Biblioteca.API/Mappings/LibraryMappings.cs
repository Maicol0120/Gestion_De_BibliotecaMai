using Gestion_De_Biblioteca.API.DTOs.Request;
using Gestion_De_Biblioteca.API.DTOs.Response;
using Gestion_De_Biblioteca.Domain.Entities;
using Gestion_De_Biblioteca.Domain.Enums;

namespace Gestion_De_Biblioteca.API.Mappings;

public static class LibraryMappings
{
    public static Author ToEntity(this AuthorRequestDto request) =>
        new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Nationality = request.Nationality,
            BirthDate = request.BirthDate
        };

    public static AuthorResponseDto ToResponse(this Author author) =>
        new(author.Id, author.FirstName, author.LastName, author.Nationality, author.BirthDate);

    public static Category ToEntity(this CategoryRequestDto request) =>
        new()
        {
            Name = request.Name,
            Description = request.Description
        };

    public static CategoryResponseDto ToResponse(this Category category) =>
        new(category.Id, category.Name, category.Description);

    public static Book ToEntity(this BookRequestDto request) =>
        new()
        {
            Title = request.Title,
            Isbn = request.Isbn,
            PublicationYear = request.PublicationYear,
            TotalCopies = request.TotalCopies,
            AvailableCopies = request.AvailableCopies,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId
        };

    public static BookResponseDto ToResponse(this Book book) =>
        new(
            book.Id,
            book.Title,
            book.Isbn,
            book.PublicationYear,
            book.TotalCopies,
            book.AvailableCopies,
            book.AuthorId,
            book.Author is null ? null : $"{book.Author.FirstName} {book.Author.LastName}",
            book.CategoryId,
            book.Category?.Name);

    public static Member ToEntity(this MemberRequestDto request) =>
        new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            IsActive = request.IsActive
        };

    public static MemberResponseDto ToResponse(this Member member) =>
        new(member.Id, member.FirstName, member.LastName, member.Email, member.Phone, member.RegistrationDate, member.IsActive);

    public static LoanResponseDto ToResponse(this Loan loan) =>
        new(
            loan.Id,
            loan.BookId,
            loan.Book?.Title,
            loan.MemberId,
            loan.Member is null ? null : $"{loan.Member.FirstName} {loan.Member.LastName}",
            loan.LoanDate,
            loan.DueDate,
            loan.ReturnDate,
            loan.LateFee,
            GetLoanStatus(loan).ToString());

    private static LoanStatus GetLoanStatus(Loan loan)
    {
        if (loan.Status == LoanStatus.Active && loan.DueDate < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return LoanStatus.Overdue;
        }

        return loan.Status;
    }
}
