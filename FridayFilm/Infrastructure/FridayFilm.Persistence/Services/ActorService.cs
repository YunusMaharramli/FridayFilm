using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.DTOs.ActorsDtos;
using FridayFilm.Application.Exceptions;
using FridayFilm.Application.Extensions;
using FridayFilm.Application.Pagination;
using FridayFilm.Domain.Entities;
using FridayFilm.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace FridayFilm.Application.Services;

public class ActorService : IActorService
{
    private readonly IActorReadRepository _readRepository;
    private readonly IActorWriteRepository _writeRepository;
    private readonly IFileService _fileService;

    public ActorService(
        IActorReadRepository readRepository,
        IActorWriteRepository writeRepository,
        [FromKeyedServices("cloudinary")] IFileService fileService)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _fileService = fileService;
    }

    public async Task<IEnumerable<ActorResponse>> GetAllAsync()
    {
        var actors = await _readRepository.GetAllAsync();

        return actors.Select(a => new ActorResponse
        {
            Id = a.Id,
            FullName = a.FullName,
            Nationality = a.Nationality,
            Gender = a.Gender,
            Nickname = a.Nickname,
            Bio = a.Bio,
            ImageId = a.ImageId,
        });
    }

    public async Task<ActorResponse> GetByIdAsync(Guid id)
    {
        var actor = await _readRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(
                $"Actor with ID '{id}' was not found.");


        return new ActorResponse
        {
            Id = actor.Id,
            FullName = actor.FullName,
            Nationality = actor.Nationality,
            Gender = actor.Gender,
            Nickname = actor.Nickname,
            Bio = actor.Bio,
            ImageId = actor.ImageId,
        };
    }

    public async Task<IEnumerable<ActorResponse>> SearchByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Actor name cannot be empty.");

        var actors = await _readRepository.GetAllAsync(
            x => x.FullName.ToLower().Contains(name.ToLower()));


        return actors.Select(a => new ActorResponse
        {
            Id = a.Id,
            FullName = a.FullName,
            Nationality = a.Nationality,
            Gender = a.Gender,
            Nickname = a.Nickname,
            Bio = a.Bio,
            ImageId = a.ImageId,
        });
    }

    public async Task<PaginatedResponse<ActorResponse>> GetAllPaginatedAsync(PaginationRequest request)
    {
        if (request.Page < 1 || request.Size < 1)
            throw new ValidationException("Page and size must be greater than zero.");

        int totalCount = await _readRepository.GetCountAsync();
        int skip = (request.Page - 1) * request.Size;

        var actors = await _readRepository.GetAllAsync(skip: skip, take: request.Size);

        var mappedData = actors.Select(a => new ActorResponse
        {
            Id = a.Id,
            FullName = a.FullName,
            Nationality = a.Nationality,
            Gender = a.Gender,
            Nickname = a.Nickname,
            Bio = a.Bio,
            ImageId = a.ImageId,
        }).ToList();

        return new PaginatedResponse<ActorResponse>(mappedData, totalCount, request.Page, request.Size);
    }

    public async Task CreateAsync(CreateActorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
            throw new ValidationException("Actor full name cannot be empty.");

        FilmImage? newImage = null;

        if (request.Photo != null)
        {
            string photoUrl = await _fileService.UploadAsync("images/actors", request.Photo);
            newImage = new FilmImage { PhotoUrl = photoUrl };
        }

        var actor = new Actor
        {
            FullName = request.FullName,
            Nationality = request.Nationality,
            Gender = request.Gender ?? Gender.Other,
            Nickname = request.Nickname,
            Bio = request.Bio,
            Slug = request.FullName.ToSlug(),
            Image = newImage
        };

        await _writeRepository.AddAsync(actor);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task UpdateAsync(Guid id, UpdateActorRequest request)
    {
        var actor = await _readRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(
                $"Actor with ID '{id}' was not found.");

        // 1. Slug və Adın yoxlanılması (Yalnız ad dəyişibsə yenilənir)
        if (!string.IsNullOrWhiteSpace(request.FullName) && actor.FullName != request.FullName)
        {
            actor.FullName = request.FullName;
            actor.Slug = request.FullName.ToSlug();
        }

        // 2. Boş (null və ya empty) gəlməyibsə köhnəni əzib yenisini yazırıq
        if (!string.IsNullOrWhiteSpace(request.Nationality))
        {
            actor.Nationality = request.Nationality;
        }

        if (request.Gender.HasValue) // Əgər enum DTO-da nullable (Gender?) formatındadırsa
        {
            actor.Gender = request.Gender.Value;
        }

        if (!string.IsNullOrWhiteSpace(request.Nickname))
        {
            actor.Nickname = request.Nickname;
        }

        if (!string.IsNullOrWhiteSpace(request.Bio))
        {
            actor.Bio = request.Bio;
        }

        // 3. Şəkil yenilənməsi prosesi
        if (request.Photo != null)
        {
            if (actor.Image != null && !string.IsNullOrEmpty(actor.Image.PhotoUrl))
            {
                 _fileService.Delete(actor.Image.PhotoUrl);
            }

            string photoUrl = await _fileService.UploadAsync("images/actors", request.Photo);
          
            if (actor.Image != null)
            {
                actor.Image.PhotoUrl = photoUrl;
            }
            else
            {
                actor.Image = new FilmImage { PhotoUrl = photoUrl };
            }
        }

        _writeRepository.Update(actor);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var actor = await _readRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(
                $"Actor with ID '{id}' was not found.");

        _writeRepository.Delete(actor);
        await _writeRepository.SaveChangeAsync();
    }
}
