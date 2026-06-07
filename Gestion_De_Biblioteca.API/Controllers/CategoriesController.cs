using Gestion_De_Biblioteca.API.DTOs.Request;
using Gestion_De_Biblioteca.API.Mappings;
using Gestion_De_Biblioteca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_De_Biblioteca.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController(ICategoryService CategoryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await CategoryService.GetCategoriesAsync(); 
        return Ok(categories.Select(category => category.ToResponse()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await CategoryService.GetCategoryAsync(id);
        return category is null ? NotFound() : Ok(category.ToResponse());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryRequestDto request)
    {
        var category = await CategoryService.CreateCategoryAsync(request.ToEntity());
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category.ToResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CategoryRequestDto request)
    {
        var category = await CategoryService.UpdateCategoryAsync(id, request.ToEntity());
        return category is null ? NotFound() : Ok(category.ToResponse());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await CategoryService.DeleteCategoryAsync(id) ? NoContent() : NotFound();
}
