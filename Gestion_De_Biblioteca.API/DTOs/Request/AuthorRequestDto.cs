namespace Gestion_De_Biblioteca.API.DTOs.Request;

public record AuthorRequestDto(string FirstName, string LastName, string? Nationality, DateOnly? BirthDate);
