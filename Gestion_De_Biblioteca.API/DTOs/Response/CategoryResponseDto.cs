namespace Gestion_De_Biblioteca.API.DTOs.Response;

public class CategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
