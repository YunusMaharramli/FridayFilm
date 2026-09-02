using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.DTOs.BioDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FridayFilm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiosController : ControllerBase
    {
        private readonly IBioService _bioService;

        public BiosController(IBioService bioService)
        {
            _bioService = bioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bios = await _bioService.GetAllAsync();
            return Ok(bios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var bio = await _bioService.GetByIdAsync(id);
            if (bio == null) return NotFound("Sayt məlumatı tapılmadı.");

            return Ok(bio);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBioRequest request)
        {
            await _bioService.CreateAsync(request);
            return StatusCode(201, "Sayt məlumatı uğurla yaradıldı");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateBioRequest request)
        {
            var result = await _bioService.UpdateAsync(id, request);
            if (!result) return NotFound("Yenilənməli olan məlumat tapılmadı.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _bioService.DeleteAsync(id);
            if (!result) return NotFound("Silinməli olan məlumat tapılmadı.");

            return NoContent();
        }
    }
}