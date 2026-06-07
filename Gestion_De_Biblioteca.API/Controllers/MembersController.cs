using Gestion_De_Biblioteca.API.DTOs.Request;
using Gestion_De_Biblioteca.API.Mappings;
using Gestion_De_Biblioteca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_De_Biblioteca.API.Controllers;

[ApiController]
[Route("api/members")]
public class MembersController(ILibraryService libraryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var members = await libraryService.GetMembersAsync();
        return Ok(members.Select(member => member.ToResponse()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var member = await libraryService.GetMemberAsync(id);
        return member is null ? NotFound() : Ok(member.ToResponse());
    }

    [HttpPost]
    public async Task<IActionResult> Create(MemberRequestDto request)
    {
        var member = await libraryService.CreateMemberAsync(request.ToEntity());
        return CreatedAtAction(nameof(GetById), new { id = member.Id }, member.ToResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, MemberRequestDto request)
    {
        var member = await libraryService.UpdateMemberAsync(id, request.ToEntity());
        return member is null ? NotFound() : Ok(member.ToResponse());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await libraryService.DeleteMemberAsync(id) ? NoContent() : NotFound();
}
