using Gestion_De_Biblioteca.API.DTOs.Request;
using Gestion_De_Biblioteca.API.Mappings;
using Gestion_De_Biblioteca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_De_Biblioteca.API.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController(ILibraryService libraryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await libraryService.GetAuthorsAsync();
        return Ok(authors.Select(author => author.ToResponse()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await libraryService.GetAuthorAsync(id);
        return author is null ? NotFound() : Ok(author.ToResponse());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuthorRequestDto request)
    {
        var author = await libraryService.CreateAuthorAsync(request.ToEntity());
        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author.ToResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AuthorRequestDto request)
    {
        var author = await libraryService.UpdateAuthorAsync(id, request.ToEntity());
        return author is null ? NotFound() : Ok(author.ToResponse());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await libraryService.DeleteAuthorAsync(id) ? NoContent() : NotFound();
}
