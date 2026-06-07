namespace Gestion_De_Biblioteca.API.DTOs.Response;

public class BookResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public string Status { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}
