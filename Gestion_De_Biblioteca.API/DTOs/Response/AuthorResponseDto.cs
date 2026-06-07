namespace Gestion_De_Biblioteca.API.DTOs.Response;

public record AuthorResponseDto(int Id, string FirstName, string LastName, string? Nationality, DateOnly? BirthDate);
