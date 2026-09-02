using FridayFilm.Application.Dtos.CategoryDtos;
using FridayFilm.Application.Pagination;

namespace FridayFilm.Application.Abstracts.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllAsync();
    Task<CategoryResponse?> GetByIdAsync(Guid id);
    Task<CategoryResponse?> GetBySlugAsync(string slug);
    Task<IEnumerable<CategoryResponse>> SearchByNameAsync(string name);
    Task CreateAsync(CreateCategoryRequest request);
    Task<bool> UpdateAsync(Guid id, UpdateCategoryRequest request); // Update bura əlavə olundu
    Task<bool> DeleteAsync(Guid id);
    Task<PaginatedResponse<CategoryResponse>> GetAllPaginatedAsync(PaginationRequest request);
}