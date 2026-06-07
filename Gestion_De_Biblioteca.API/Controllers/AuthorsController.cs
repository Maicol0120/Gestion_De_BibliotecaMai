using Gestion_De_Biblioteca.API.DTOs.Request;
using Gestion_De_Biblioteca.API.Mappings;
using Gestion_De_Biblioteca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_De_Biblioteca.API.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController(ILibraryService LibraryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await LibraryService.GetAuthorsAsync();
        return Ok(authors.Select(author => author.ToResponse()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await LibraryService.GetAuthorAsync(id);
        return author is null ? NotFound() : Ok(author.ToResponse());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuthorRequestDto request)
    {
        var author = await LibraryService.CreateAuthorAsync(request.ToEntity());
        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author.ToResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AuthorRequestDto request)
    {
        var author = await LibraryService.UpdateAuthorAsync(id, request.ToEntity());
        return author is null ? NotFound() : Ok(author.ToResponse());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await LibraryService.DeleteAuthorAsync(id) ? NoContent() : NotFound();
}
