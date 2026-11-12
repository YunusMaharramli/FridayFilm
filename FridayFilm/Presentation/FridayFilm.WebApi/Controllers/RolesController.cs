using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.Dtos.RoleDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FridayFilm.WebApi.Controllers;

[ApiController]
[Route("api/roles")]
[Authorize]
public sealed class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [Authorize(Policy = Permissions.Roles.Read)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _roleService.GetAllAsync();
        return Ok(roles);
    }

    [HttpGet("permissions")]
    public IActionResult GetAllPermissions()
    {
        return Ok(Permissions.All);
    }

    [Authorize(Policy = Permissions.Roles.Create)]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateRoleRequest request)
    {
        await _roleService.CreateAsync(request);

        return StatusCode(
            StatusCodes.Status201Created,
            "Rol uğurla yaradıldı.");
    }

    [Authorize(Policy = Permissions.Roles.Update)]
    [HttpPut("{roleId}")]
    public async Task<IActionResult> Update(
        string roleId,
        [FromBody] UpdateRoleRequest request)
    {
        await _roleService.UpdateAsync(
            roleId,
            request);

        return Ok("Rol uğurla yeniləndi.");
    }
}