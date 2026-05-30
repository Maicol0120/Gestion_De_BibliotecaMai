using Gestion_De_Biblioteca.Domain.Enums;

namespace Gestion_De_Biblioteca.Domain.Entities;

public class Loan
{
    public int Id { get; set; }
    public DateOnly LoanDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public DateOnly DueDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
    public decimal LateFee { get; set; }
    public LoanStatus Status { get; set; } = LoanStatus.Active;

    public int BookId { get; set; }
    public Book? Book { get; set; }

    public int MemberId { get; set; }
    public Member? Member { get; set; }

    public bool IsReturned => Status == LoanStatus.Returned;
}
