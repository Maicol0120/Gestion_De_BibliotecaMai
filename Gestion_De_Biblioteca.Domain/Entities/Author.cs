namespace Gestion_De_Biblioteca.Domain.Entities;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Nationality { get; set; }
    public DateOnly? BirthDate { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
