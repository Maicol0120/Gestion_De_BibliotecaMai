namespace Gestion_De_Biblioteca.API.DTOs.Response;

public record LoanResponseDto(
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
