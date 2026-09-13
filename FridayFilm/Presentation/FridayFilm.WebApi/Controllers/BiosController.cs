using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.DTOs.BioDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FridayFilm.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BiosController : ControllerBase
{
    private readonly IBioService _bioService;

    public BiosController(IBioService bioService)
    {
        _bioService = bioService;
    }

    [Authorize(Policy = Permissions.Bios.Read)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bios = await _bioService.GetAllAsync();
        return Ok(bios);
    }

    [Authorize(Policy = Permissions.Bios.Read)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var bio = await _bioService.GetByIdAsync(id);
        return Ok(bio);
    }

    [Authorize(Policy = Permissions.Bios.Create)]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateBioRequest request)
    {
        await _bioService.CreateAsync(request);
        return StatusCode(StatusCodes.Status201Created, "Sayt məlumatı uğurla yaradıldı.");
    }

    [Authorize(Policy = Permissions.Bios.Update)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateBioRequest request)
    {
        await _bioService.UpdateAsync(id, request);
        return NoContent();
    }

    [Authorize(Policy = Permissions.Bios.Delete)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _bioService.DeleteAsync(id);
        return NoContent();
    }
}
