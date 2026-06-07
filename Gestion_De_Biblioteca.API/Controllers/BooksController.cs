using Gestion_De_Biblioteca.API.DTOs.Request;
using Gestion_De_Biblioteca.API.Mappings;
using Gestion_De_Biblioteca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_De_Biblioteca.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController(ILibraryService libraryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await libraryService.GetBooksAsync();
        return Ok(books.Select(book => book.ToResponse()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await libraryService.GetBookAsync(id);
        return book is null ? NotFound() : Ok(book.ToResponse());
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchByCategory([FromQuery] string? category)
    {
        var books = await libraryService.SearchBooksByCategoryAsync(category);
        return Ok(books.Select(book => book.ToResponse()));
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookRequestDto request)
    {
        var result = await libraryService.CreateBookAsync(request.ToEntity());
        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data.ToResponse())
            : BadRequest(result.Error);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, BookRequestDto request)
    {
        var result = await libraryService.UpdateBookAsync(id, request.ToEntity());
        return result.Success ? Ok(result.Data!.ToResponse()) : BadRequest(result.Error);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await libraryService.DeleteBookAsync(id) ? NoContent() : NotFound();
}
