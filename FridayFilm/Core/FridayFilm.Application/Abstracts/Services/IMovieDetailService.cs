using FridayFilm.Application.Dtos.MovieDetailDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Application.Abstracts.Services;

public interface IMovieDetailService
{
    Task<IEnumerable<MovieDetailResponse>> GetAllAsync();
    Task<MovieDetailResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateMovieDetailRequest request);
    Task<bool> UpdateAsync(Guid id, UpdateMovieDetailRequest request);
    Task<bool> DeleteAsync(Guid id);
}
