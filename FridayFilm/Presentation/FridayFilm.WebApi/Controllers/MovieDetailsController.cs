using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.Dtos.MovieDetailDtos;
using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var details = await _movieDetailService.GetAllAsync();
        return Ok(details);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var detail = await _movieDetailService.GetByIdAsync(id);
        if (detail == null) throw new FridayFilm.Application.Exceptions.NotFoundException("Film detalı tapılmadı.");

        return Ok(detail);
    }

}
