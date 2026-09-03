using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.DTOs.ActorsDtos;
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

    public async Task<ActorResponse?> GetByIdAsync(Guid id)
    {
        var actor = await _readRepository.GetByIdAsync(id);
        if (actor == null) return null;

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
        var actors = await _readRepository.GetAllAsync(x => x.FullName.ToLower().Contains(name.ToLower()));

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

    public async Task<bool> UpdateAsync(Guid id, UpdateActorRequest request)
    {
        var actor = await _readRepository.GetByIdAsync(id);
        if (actor == null) return false;

        // 1. Digər məlumatların tam (full) yenilənməsi
        actor.FullName = request.FullName;
        actor.Nationality = request.Nationality;
        actor.Gender = request.Gender;
        actor.Nickname = request.Nickname;
        actor.Bio = request.Bio;
        actor.Slug = request.FullName.ToSlug();

        // 2. Şəkil yenilənməsi prosesi
        if (request.Photo != null)
        {
            // Əgər aktyorun əvvəlcədən şəkli var idisə, köhnəni silirik
            if (actor.Image != null && !string.IsNullOrEmpty(actor.Image.PhotoUrl))
            {
                // Qeyd: IFileService-in içində DeleteAsync metodu yaratdığına əmin ol.
                 _fileService.Delete(actor.Image.PhotoUrl);
            }

            // Yeni şəkli Cloudinary-ə yükləyirik
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

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var actor = await _readRepository.GetByIdAsync(id);
        if (actor == null) return false;

        _writeRepository.Delete(actor);
        await _writeRepository.SaveChangeAsync();

        return true;
    }
}