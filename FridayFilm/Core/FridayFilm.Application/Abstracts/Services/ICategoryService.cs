using FridayFilm.Domain.Entities;

namespace FridayFilm.Application.Abstracts.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task<Category?> GetBySlugAsync(string slug);
    Task<IEnumerable<Category>> SearchByNameAsync(string name);
    Task CreateAsync(Category category);
    Task<bool> DeleteAsync(Guid id);
}