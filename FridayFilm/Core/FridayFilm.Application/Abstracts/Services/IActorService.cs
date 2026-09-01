using FridayFilm.Application.Dtos.CategoryDtos;
using FridayFilm.Application.DTOs.ActorsDtos;
using FridayFilm.Application.Pagination;
// Pagination DTO-larının olduğu namespace-i bura əlavə et (məsələn: using FridayFilm.Application.DTOs.Common;)

namespace FridayFilm.Application.Abstracts.Services;

public interface IActorService
{
    Task<PaginatedResponse<ActorResponse>> GetAllPaginatedAsync(PaginationRequest request); 
    Task<ActorResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateActorRequest request);
    Task<bool> UpdateAsync(Guid id, UpdateActorRequest request);
    Task<IEnumerable<ActorResponse>> SearchByNameAsync(string name);

    Task<bool> DeleteAsync(Guid id);
}