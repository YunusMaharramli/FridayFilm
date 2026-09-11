using FridayFilm.Application.Dtos.MovieDtos;
using FridayFilm.Application.Pagination;

namespace FridayFilm.Application.Abstracts.Services;

public interface IMovieService
{
    Task<Guid> CreateAsync(CreateMovieRequest request, CancellationToken cancellationToken = default);
    Task<PaginatedResponse<MovieResponse>> GetAllAsync(PaginationRequest request, CancellationToken cancellationToken = default);
    Task<MovieResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, UpdateMovieRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
