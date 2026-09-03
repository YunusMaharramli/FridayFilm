using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.DTOs.ActorsDtos;
using FridayFilm.Application.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace FridayFilm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        // GET api/Actors (Pagination olan əsas metod)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
        {
            var response = await _actorService.GetAllPaginatedAsync(request);
            return Ok(response);
        }

       

        // GET api/Actors/5c60f693-8f5e-40c9-9400-08db3b210000
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _actorService.GetByIdAsync(id);
            return Ok(response);
        }

        

        // GET api/Actors/search?name=leonardo
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var response = await _actorService.SearchByNameAsync(name);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateActorRequest request)
        {
            await _actorService.CreateAsync(request);
            return StatusCode(201, "Aktyor uğurla yaradıldı.");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] UpdateActorRequest request)
        {
            await _actorService.UpdateAsync(id, request);
            return Ok("Aktyor uğurla yeniləndi.");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _actorService.DeleteAsync(id);
            return Ok("Aktyor uğurla silindi.");
        }
    }
}
