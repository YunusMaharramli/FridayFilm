using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FridayFilm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return category == null ? NotFound("Kateqoriya tapılmadı.") : Ok(category);
        }

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var category = await _categoryService.GetBySlugAsync(slug);
            return category == null ? NotFound("Bu linkə uyğun kateqoriya tapılmadı.") : Ok(category);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Axtarış üçün kateqoriya adı daxil edilməlidir.");

            var categories = await _categoryService.SearchByNameAsync(name);
            return !categories.Any() ? NotFound("Bu ada uyğun kateqoriya tapılmadı.") : Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            await _categoryService.CreateAsync(category);
            return StatusCode(201, "Kateqoriya uğurla yaradıldı.");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return result ? Ok("Kateqoriya uğurla silindi.") : NotFound("Kateqoriya tapılmadı.");
        }
    }
}