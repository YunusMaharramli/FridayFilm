using FridayFilm.Application.Dtos.GenreDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Application.Abstracts.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponse>> GetAllAsync();
        Task<GenreResponse?> GetByIdAsync(Guid id);
        Task CreateAsync(CreateGenreRequest request);
        Task<bool> UpdateAsync(Guid id, UpdateGenreRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
