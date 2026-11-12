using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Dtos.MovieDetailDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FridayFilm.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieDetailsController : ControllerBase
{
    private readonly IMovieDetailService _movieDetailService;

    public MovieDetailsController(IMovieDetailService movieDetailService)
    {
        _movieDetailService = movieDetailService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var details = await _movieDetailService.GetAllAsync();
        return Ok(details);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var detail = await _movieDetailService.GetByIdAsync(id);
        if (detail == null) return NotFound("Film detalı tapılmadı.");

        return Ok(detail);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMovieDetailRequest request)
    {
        await _movieDetailService.CreateAsync(request);
        return StatusCode(201, "Film detalı uğurla yaradıldı.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieDetailRequest request)
    {
        var result = await _movieDetailService.UpdateAsync(id, request);
        if (!result) return NotFound("Yenilənmək üçün film detalı tapılmadı.");

        return Ok("Film detalı uğurla yeniləndi.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _movieDetailService.DeleteAsync(id);
        if (!result) return NotFound("Silinmək üçün film detalı tapılmadı.");

        return Ok("Film detalı uğurla silindi.");
    }
}