using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Dtos.CategoryDtos;
using FridayFilm.Application.Exceptions;
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

    public async Task<CategoryResponse> GetByIdAsync(Guid id)
    {
        var category = await _readRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(
                $"Category with ID '{id}' was not found.");

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            CreatedDate = category.CreatedDate
        };
    }

    public async Task<CategoryResponse> GetBySlugAsync(string slug)
    {
        var category = await _readRepository.GetAsync(
            x => x.Slug == slug)
            ?? throw new NotFoundException(
                $"Category with slug '{slug}' was not found.");

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            CreatedDate = category.CreatedDate
        };
    }

    public async Task<IEnumerable<CategoryResponse>> SearchByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Category name cannot be empty.");

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
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Category name cannot be empty.");

        var category = new Category
        {
            Name = request.Name,
            Slug = request.Name.ToSlug() // Extension metod işə düşür
        };

        await _writeRepository.AddAsync(category);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task UpdateAsync(Guid id, UpdateCategoryRequest request)
    {
        var category = await _readRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(
                $"Category with ID '{id}' was not found.");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Category name cannot be empty.");

        category.Name = request.Name;
        category.Slug = request.Name.ToSlug(); 

        _writeRepository.Update(category);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _readRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(
                $"Category with ID '{id}' was not found.");

        _writeRepository.Delete(category);
        await _writeRepository.SaveChangeAsync();
    }
    public async Task<PaginatedResponse<CategoryResponse>> GetAllPaginatedAsync(PaginationRequest request)
    {
        if (request.Page < 1 || request.Size < 1)
            throw new ValidationException("Page and size must be greater than zero.");

       
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
