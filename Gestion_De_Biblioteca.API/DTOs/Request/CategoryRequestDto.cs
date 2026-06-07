namespace Gestion_De_Biblioteca.API.DTOs.Request;

public class CategoryRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
