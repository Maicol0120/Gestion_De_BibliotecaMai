namespace Gestion_De_Biblioteca.API.Dtos;

public record AuthorRequest(string FirstName, string LastName, string? Nationality, DateOnly? BirthDate);
public record AuthorResponse(int Id, string FirstName, string LastName, string? Nationality, DateOnly? BirthDate);

public record CategoryRequest(string Name, string? Description);
public record CategoryResponse(int Id, string Name, string? Description);

public record BookRequest(
    string Title,
    string Isbn,
    int PublicationYear,
    int TotalCopies,
    int AvailableCopies,
    int AuthorId,
    int CategoryId);

public record BookResponse(
    int Id,
    string Title,
    string Isbn,
    int PublicationYear,
    int TotalCopies,
    int AvailableCopies,
    int AuthorId,
    string? AuthorName,
    int CategoryId,
    string? CategoryName);

public record MemberRequest(string FirstName, string LastName, string Email, string? Phone, bool IsActive);
public record MemberResponse(int Id, string FirstName, string LastName, string Email, string? Phone, DateOnly RegistrationDate, bool IsActive);

public record LoanRequest(int BookId, int MemberId, DateOnly DueDate);
public record ReturnLoanRequest(DateOnly? ReturnDate);

public record LoanResponse(
    int Id,
    int BookId,
    string? BookTitle,
    int MemberId,
    string? MemberName,
    DateOnly LoanDate,
    DateOnly DueDate,
    DateOnly? ReturnDate,
    decimal LateFee,
    string Status);
