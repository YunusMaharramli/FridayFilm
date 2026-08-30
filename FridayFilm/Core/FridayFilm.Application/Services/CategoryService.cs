using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Extensions;
using FridayFilm.Domain.Entities;

namespace FridayFilm.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly ICategoryWriteRepository _writeRepository;

    public CategoryService(
        ICategoryReadRepository readRepository,
        ICategoryWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _readRepository.GetAllAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _readRepository.GetByIdAsync(id);
    }

    public async Task<Category?> GetBySlugAsync(string slug)
    {
        return await _readRepository.GetAsync(x => x.Slug == slug);
    }

    public async Task<IEnumerable<Category>> SearchByNameAsync(string name)
    {
        return await _readRepository.GetAllAsync(x => x.Name.Contains(name));
    }

    public async Task CreateAsync(Category category)
    {
        category.Slug = category.Name.ToSlug(); 
        await _writeRepository.AddAsync(category);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _readRepository.GetByIdAsync(id);
        if (category == null)
            return false;

        _writeRepository.Delete(category);
        await _writeRepository.SaveChangeAsync();
        return true;
    }
}