namespace Gestion_De_Biblioteca.API.DTOs.Request;

public class LoanRequestDto
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateOnly DueDate { get; set; }
}
