using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.DTOs.DirectorsDtos;
using FridayFilm.Application.Extensions;
using FridayFilm.Application.Pagination;
using FridayFilm.Domain.Entities;
using FridayFilm.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridayFilm.Application.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorReadRepository _readRepository;
        private readonly IDirectorWriteRepository _writeRepository;
        private readonly IFileService _fileService;

        public DirectorService(
            IDirectorReadRepository readRepository,
            IDirectorWriteRepository writeRepository,
            [FromKeyedServices("cloudinary")] IFileService fileService)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _fileService = fileService;
        }

        public async Task<IEnumerable<DirectorResponse>> GetAllAsync()
        {
            var directors = await _readRepository.GetAllAsync();

            return directors.Select(d => new DirectorResponse
            {
                Id = d.Id,
                FullName = d.FullName,
                Nationality = d.Nationality,
                Gender = d.Gender,
                Bio = d.Bio,
                ImageId = d.ImageId,
                Slug = d.Slug
            });
        }

        public async Task<DirectorResponse?> GetByIdAsync(Guid id)
        {
            var director = await _readRepository.GetByIdAsync(id);

            if (director == null) return null;

            return new DirectorResponse
            {
                Id = director.Id,
                FullName = director.FullName,
                Nationality = director.Nationality,
                Gender = director.Gender,
                Bio = director.Bio,
                ImageId = director.ImageId,
                Slug = director.Slug
            };
        }

        public async Task<DirectorResponse?> GetBySlugAsync(string slug)
        {
            var director = await _readRepository.GetAsync(x => x.Slug == slug);
            if (director == null) return null;

            return new DirectorResponse
            {
                Id = director.Id,
                FullName = director.FullName,
                Nationality = director.Nationality,
                Gender = director.Gender,
                Bio = director.Bio,
                ImageId = director.ImageId,
                Slug = director.Slug
            };
        }

        public async Task<IEnumerable<DirectorResponse>> SearchByNameAsync(string name)
        {
            var directors = await _readRepository.GetAllAsync(x => x.FullName.ToLower().Contains(name.ToLower()));

            return directors.Select(d => new DirectorResponse
            {
                Id = d.Id,
                FullName = d.FullName,
                Nationality = d.Nationality,
                Gender = d.Gender,
                Bio = d.Bio,
                ImageId = d.ImageId,
                Slug = d.Slug
            });
        }

        public async Task<PaginatedResponse<DirectorResponse>> GetAllPaginatedAsync(PaginationRequest request)
        {
            int totalCount = await _readRepository.GetCountAsync();
            int skip = (request.Page - 1) * request.Size;

            var directors = await _readRepository.GetAllAsync(skip: skip, take: request.Size);

            var mappedData = directors.Select(d => new DirectorResponse
            {
                Id = d.Id,
                FullName = d.FullName,
                Nationality = d.Nationality,
                Gender = d.Gender,
                Bio = d.Bio,
                ImageId = d.ImageId,
                Slug = d.Slug
            }).ToList();

            return new PaginatedResponse<DirectorResponse>(mappedData, totalCount, request.Page, request.Size);
        }

        public async Task CreateAsync(CreateDirectorRequest request)
        {
            FilmImage? newImage = null;

            if (request.Photo != null)
            {
                string photoUrl = await _fileService.UploadAsync("images/directors", request.Photo);
                newImage = new FilmImage { PhotoUrl = photoUrl };
            }

            var director = new Director
            {
                FullName = request.FullName,
                Nationality = request.Nationality,
                Gender = request.Gender ?? Gender.Other,
                Bio = request.Bio,
                Slug = request.FullName.ToSlug(),
                Image = newImage
            };

            await _writeRepository.AddAsync(director);
            await _writeRepository.SaveChangeAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateDirectorRequest request)
        {
            var director = await _readRepository.GetByIdAsync(id);
            if (director == null) return false;

            director.FullName = request.FullName;
            director.Nationality = request.Nationality;
            director.Gender = request.Gender ?? director.Gender;
            director.Bio = request.Bio;
            director.Slug = request.FullName.ToSlug();

            if (request.Photo != null)
            {
                string photoUrl = await _fileService.UploadAsync("images/directors", request.Photo);
                director.Image = new FilmImage { PhotoUrl = photoUrl };
            }

            _writeRepository.Update(director);
            await _writeRepository.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var director = await _readRepository.GetByIdAsync(id);
            if (director == null) return false;

            _writeRepository.Delete(director);
            await _writeRepository.SaveChangeAsync();

            return true;
        }
    }
}