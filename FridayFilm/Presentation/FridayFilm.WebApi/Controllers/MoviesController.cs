using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.Dtos.MovieDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FridayFilm.Application.Pagination;

namespace FridayFilm.WebApi.Controllers;

[ApiController]
[Route("api/movies")]
public sealed class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpPost("cover")]
    [Authorize]
    [RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<IActionResult> UploadCover(IFormFile file, [FromKeyedServices("cloudinary")] IFileService files)
    {
        if (!User.HasClaim(CustomClaimTypes.Permission, Permissions.Movies.Create) &&
            !User.HasClaim(CustomClaimTypes.Permission, Permissions.Movies.Update))
            return Forbid();
        if (file.Length is <= 0 or > 5 * 1024 * 1024 ||
            file.ContentType is not ("image/jpeg" or "image/png" or "image/webp"))
            throw new FridayFilm.Application.Exceptions.ValidationException("Maksimum 5 MB JPEG, PNG və ya WebP seçin.");
        return Ok(new { Url = await files.UploadAsync("movies", file) });
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Movies.Create)]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request, CancellationToken cancellationToken)
    {
        var id = await _movieService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] MovieQueryRequest request, CancellationToken cancellationToken)
        => Ok(await _movieService.GetAllAsync(request, cancellationToken));

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => Ok(await _movieService.GetByIdAsync(id, cancellationToken));

    [Authorize(Policy = Permissions.Movies.Update)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieRequest request, CancellationToken cancellationToken)
    {
        await _movieService.UpdateAsync(id, request, cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = Permissions.Movies.Delete)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _movieService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
