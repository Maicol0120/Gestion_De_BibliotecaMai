using Gestion_De_Biblioteca.Domain.Enums;

namespace Gestion_De_Biblioteca.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public BookStatus Status { get; set; } = BookStatus.Available;

    public int AuthorId { get; set; }
    public Author? Author { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
