namespace Gestion_De_Biblioteca.API.DTOs.Response;

public record BookResponseDto(
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
