namespace Gestion_De_Biblioteca.API.DTOs.Request;

public record BookRequestDto(
    string Title,
    string Isbn,
    int PublicationYear,
    int TotalCopies,
    int AvailableCopies,
    int AuthorId,
    int CategoryId);
