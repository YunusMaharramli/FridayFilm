using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.DTOs.DirectorsDtos;
using FridayFilm.Application.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace FridayFilm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectorService _directorService;

        public DirectorsController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

     
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
        {
            var response = await _directorService.GetAllPaginatedAsync(request);
            return Ok(response);
        }

        // GET api/Directors/22222222-3333-4444-5555-666666666601
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _directorService.GetByIdAsync(id);
            return Ok(response);
        }

        // GET api/Directors/slug/christopher-nolan
        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var response = await _directorService.GetBySlugAsync(slug);
            return Ok(response);
        }

        // GET api/Directors/search?name=nolan
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var response = await _directorService.SearchByNameAsync(name);
            return Ok(response);
        }

        // POST api/Directors
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateDirectorRequest request)
        {
            await _directorService.CreateAsync(request);
            return StatusCode(201, "Director was created successfully.");
        }

        // PUT api/Directors/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] UpdateDirectorRequest request)
        {
            await _directorService.UpdateAsync(id, request);
            return Ok("Director was updated successfully.");
        }

        // DELETE api/Directors/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _directorService.DeleteAsync(id);
            return Ok("Director was deleted successfully.");
        }
    }
}
