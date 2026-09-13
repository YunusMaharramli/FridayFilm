using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.Dtos.GenreDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace FridayFilm.WebApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var genre = await _genreService.GetByIdAsync(id);
            if (genre == null) throw new FridayFilm.Application.Exceptions.NotFoundException("Janr tapılmadı.");

            return Ok(genre);
        }

        [Authorize(Policy = Permissions.Genres.Create)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGenreRequest request)
        {
            await _genreService.CreateAsync(request);
            return StatusCode(201, "Janr uğurla yaradıldı.");
        }

        [Authorize(Policy = Permissions.Genres.Update)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGenreRequest request)
        {
            var result = await _genreService.UpdateAsync(id, request);
            if (!result) throw new FridayFilm.Application.Exceptions.NotFoundException("Yenilənmək üçün janr tapılmadı.");

            return Ok("Janr uğurla yeniləndi.");
        }

        [Authorize(Policy = Permissions.Genres.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _genreService.DeleteAsync(id);
            if (!result) throw new FridayFilm.Application.Exceptions.NotFoundException("Silinmək üçün janr tapılmadı.");

            return Ok("Janr uğurla silindi.");
        }
    }
