using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.Dtos.CategoryDtos;
using FridayFilm.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FridayFilm.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Authorize(Policy = Permissions.Categories.Read)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
    {
        var result = await _categoryService.GetAllPaginatedAsync(request);
        return Ok(result);
    }

    [Authorize(Policy = Permissions.Categories.Read)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return Ok(category);
    }

    [Authorize(Policy = Permissions.Categories.Read)]
    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var category = await _categoryService.GetBySlugAsync(slug);
        return Ok(category);
    }

    [Authorize(Policy = Permissions.Categories.Read)]
    [HttpGet("search")]
    public async Task<IActionResult> SearchByName([FromQuery] string name)
    {
        var categories = await _categoryService.SearchByNameAsync(name);
        return Ok(categories);
    }

    [Authorize(Policy = Permissions.Categories.Create)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        await _categoryService.CreateAsync(request);
        return StatusCode(StatusCodes.Status201Created, "Kateqoriya uğurla yaradıldı.");
    }

    [Authorize(Policy = Permissions.Categories.Update)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        await _categoryService.UpdateAsync(id, request);
        return Ok("Kateqoriya uğurla yeniləndi.");
    }

    [Authorize(Policy = Permissions.Categories.Delete)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.DeleteAsync(id);
        return Ok("Kateqoriya uğurla silindi.");
    }
}
