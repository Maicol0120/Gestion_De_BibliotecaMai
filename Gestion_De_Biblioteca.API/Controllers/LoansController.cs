using Gestion_De_Biblioteca.API.DTOs.Request;
using Gestion_De_Biblioteca.API.Mappings;
using Gestion_De_Biblioteca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_De_Biblioteca.API.Controllers;

[ApiController]
[Route("api/loans")]
public class LoansController(ILibraryService libraryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHistory()
    {
        var loans = await libraryService.GetLoanHistoryAsync();
        return Ok(loans.Select(loan => loan.ToResponse()));
    }

    [HttpGet("member/{memberId:int}")]
    public async Task<IActionResult> GetHistoryByMember(int memberId)
    {
        var loans = await libraryService.GetLoanHistoryByMemberAsync(memberId);
        return Ok(loans.Select(loan => loan.ToResponse()));
    }

    [HttpPost]
    public async Task<IActionResult> Create(LoanRequestDto request)
    {
        var result = await libraryService.CreateLoanAsync(request.BookId, request.MemberId, request.DueDate);
        return result.Success
            ? CreatedAtAction(nameof(GetHistory), new { id = result.Data!.Id }, result.Data.ToResponse())
            : BadRequest(result.Error);
    }

    [HttpPut("{id:int}/return")]
    public async Task<IActionResult> ReturnLoan(int id, ReturnLoanRequestDto request)
    {
        var result = await libraryService.ReturnLoanAsync(id, request.ReturnDate);
        return result.Success ? Ok(result.Data!.ToResponse()) : BadRequest(result.Error);
    }
}
