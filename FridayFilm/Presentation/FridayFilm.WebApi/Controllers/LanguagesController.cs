using FridayFilm.Persistence.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FridayFilm.WebApi.Controllers;

[ApiController]
[Route("api/languages")]
public sealed class LanguagesController(FridayFilmDbContext context) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var languages = await context.Languages.AsNoTracking().Where(x => !x.IsDeleted)
            .OrderBy(x => x.Lang).Select(x => new { x.Id, x.Lang }).ToListAsync(cancellationToken);
        return Ok(languages.Select(x => new { x.Id, Name = x.Lang.ToString() }));
    }
}
