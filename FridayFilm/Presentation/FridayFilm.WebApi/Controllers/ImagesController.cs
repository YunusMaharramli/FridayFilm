using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Pagination;
using FridayFilm.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FridayFilm.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImagesController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationRequest request,
        [FromQuery] ImageTypeFilter? type)
    {
        var result = await _imageService.GetAllPaginatedAsync(request, type);
        return Ok(result);
    }
}