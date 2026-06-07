namespace Gestion_De_Biblioteca.API.DTOs.Request;

public class AuthorRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Nationality { get; set; }
    public string? Biography { get; set; }
    public DateOnly? BirthDate { get; set; }
}
