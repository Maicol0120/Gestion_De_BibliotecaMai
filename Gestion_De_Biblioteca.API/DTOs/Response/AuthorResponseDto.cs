namespace Gestion_De_Biblioteca.API.DTOs.Response;

public class AuthorResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Nationality { get; set; }
    public string? Biography { get; set; }
    public DateOnly? BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
