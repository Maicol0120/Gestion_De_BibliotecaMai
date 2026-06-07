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
            Biography = request.Biography,
            BirthDate = request.BirthDate
        };

    public static AuthorResponseDto ToResponse(this Author author) =>
        new()
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Nationality = author.Nationality,
            Biography = author.Biography,
            BirthDate = author.BirthDate,
            CreatedAt = author.CreatedAt,
            UpdatedAt = author.UpdatedAt
        };

    public static Category ToEntity(this CategoryRequestDto request) =>
        new()
        {
            Name = request.Name,
            Description = request.Description
        };

    public static CategoryResponseDto ToResponse(this Category category) =>
        new()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };

    public static Book ToEntity(this BookRequestDto request) =>
        new()
        {
            Title = request.Title,
            Isbn = request.Isbn,
            PublicationYear = request.PublicationYear,
            TotalCopies = request.TotalCopies,
            AvailableCopies = request.AvailableCopies,
            Status = request.Status,
            AuthorId = request.AuthorId,
            CategoryId = request.CategoryId
        };

    public static BookResponseDto ToResponse(this Book book) =>
        new()
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.Isbn,
            PublicationYear = book.PublicationYear,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.AvailableCopies,
            Status = book.Status.ToString(),
            AuthorId = book.AuthorId,
            AuthorName = book.Author is null ? null : $"{book.Author.FirstName} {book.Author.LastName}",
            CategoryId = book.CategoryId,
            CategoryName = book.Category?.Name
        };

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
        new()
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            Phone = member.Phone,
            RegistrationDate = member.RegistrationDate,
            IsActive = member.IsActive
        };

    public static LoanResponseDto ToResponse(this Loan loan) =>
        new()
        {
            Id = loan.Id,
            BookId = loan.BookId,
            BookTitle = loan.Book?.Title,
            MemberId = loan.MemberId,
            MemberName = loan.Member is null ? null : $"{loan.Member.FirstName} {loan.Member.LastName}",
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
            ReturnDate = loan.ReturnDate,
            LateFee = loan.LateFee,
            Status = GetLoanStatus(loan).ToString()
        };

    private static LoanStatus GetLoanStatus(Loan loan)
    {
        if (loan.Status == LoanStatus.Active && loan.DueDate < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return LoanStatus.Overdue;
        }

        return loan.Status;
    }
}
