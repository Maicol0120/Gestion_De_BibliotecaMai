namespace Gestion_De_Biblioteca.Domain.Entities;

public class Author : AuditBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Nationality { get; set; }
    public string? Biography { get; set; }
    public DateOnly? BirthDate { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
