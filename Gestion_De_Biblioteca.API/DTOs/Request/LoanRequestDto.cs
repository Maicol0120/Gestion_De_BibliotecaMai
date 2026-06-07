namespace Gestion_De_Biblioteca.API.DTOs.Request;

public record LoanRequestDto(int BookId, int MemberId, DateOnly DueDate);
