using FridayFilm.Application.DTOs.DirectorsDtos;
using FridayFilm.Application.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Application.Abstracts.Services;

public interface IDirectorService
{
    Task<IEnumerable<DirectorResponse>> GetAllAsync();
    Task<PaginatedResponse<DirectorResponse>> GetAllPaginatedAsync(PaginationRequest request);
    Task<DirectorResponse?> GetByIdAsync(Guid id);
    Task<DirectorResponse?> GetBySlugAsync(string slug);
    Task<IEnumerable<DirectorResponse>> SearchByNameAsync(string name);
    Task CreateAsync(CreateDirectorRequest request);
    Task<bool> UpdateAsync(Guid id, UpdateDirectorRequest request);
    Task<bool> DeleteAsync(Guid id);
}
