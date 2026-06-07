namespace Gestion_De_Biblioteca.API.DTOs.Response;

public record MemberResponseDto(int Id, string FirstName, string LastName, string Email, string? Phone, DateOnly RegistrationDate, bool IsActive);
