namespace Gestion_De_Biblioteca.API.DTOs.Response;

public class MemberResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public DateOnly RegistrationDate { get; set; }
    public bool IsActive { get; set; }
}
