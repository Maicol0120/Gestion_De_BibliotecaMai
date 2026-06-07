namespace Gestion_De_Biblioteca.API.DTOs.Request;

public record MemberRequestDto(string FirstName, string LastName, string Email, string? Phone, bool IsActive);
