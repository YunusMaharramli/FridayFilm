using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.DTOs.Categories;
using FridayFilm.Application.Extensions;
using FridayFilm.Application.Pagination;
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

    public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {
        var categories = await _readRepository.GetAllAsync();

        return categories.Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            CreatedDate = c.CreatedDate
        });
    }

    public async Task<CategoryResponse?> GetByIdAsync(Guid id)
    {
        var c = await _readRepository.GetByIdAsync(id);
        if (c == null) return null;

        return new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            CreatedDate = c.CreatedDate
        };
    }

    public async Task<CategoryResponse?> GetBySlugAsync(string slug)
    {
        var c = await _readRepository.GetAsync(x => x.Slug == slug);
        if (c == null) return null;

        return new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            CreatedDate = c.CreatedDate
        };
    }

    public async Task<IEnumerable<CategoryResponse>> SearchByNameAsync(string name)
    {
        var categories = await _readRepository.GetAllAsync(x => x.Name.Contains(name));

        return categories.Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            CreatedDate = c.CreatedDate
        });
    }

    public async Task CreateAsync(CreateCategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name,
            Slug = request.Name.ToSlug() // Extension metod işə düşür
        };

        await _writeRepository.AddAsync(category);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateCategoryRequest request)
    {
        var category = await _readRepository.GetByIdAsync(id);
        if (category == null)
            return false;

        category.Name = request.Name;
        category.Slug = request.Name.ToSlug(); // Ad dəyişirsə, Slug da yenilənir

        _writeRepository.Update(category);
        await _writeRepository.SaveChangeAsync();
        return true;
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
    public async Task<PaginatedResponse<CategoryResponse>> GetAllPaginatedAsync(PaginationRequest request)
    {
       
        int totalCount = await _readRepository.GetCountAsync();

     
        int skip = (request.Page - 1) * request.Size;

     
        var categories = await _readRepository.GetAllAsync(skip: skip, take: request.Size);

        var mappedData = categories.Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            CreatedDate = c.CreatedDate
        }).ToList();

     
        return new PaginatedResponse<CategoryResponse>(mappedData, totalCount, request.Page, request.Size);
    }
}