namespace Gestion_De_Biblioteca.API.DTOs.Response;

public class LoanResponseDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string? BookTitle { get; set; }
    public int MemberId { get; set; }
    public string? MemberName { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
    public decimal LateFee { get; set; }
    public string Status { get; set; } = string.Empty;
}
