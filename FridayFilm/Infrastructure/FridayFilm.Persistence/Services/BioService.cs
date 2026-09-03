using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.DTOs.BioDtos;
using FridayFilm.Application.Exceptions;
using FridayFilm.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridayFilm.Application.Services
{
    public class BioService : IBioService
    {
        private readonly IBioReadRepository _readRepository;
        private readonly IBioWriteRepository _writeRepository;
        private readonly IFileService _fileService;

        public BioService(
            IBioReadRepository readRepository,
            IBioWriteRepository writeRepository,
            [FromKeyedServices("cloudinary")] IFileService fileService)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _fileService = fileService;
        }

        public async Task<IEnumerable<BioResponse>> GetAllAsync()
        {
            var bios = await _readRepository.GetAllAsync();

            return bios.Select(b => new BioResponse
            {
                Id = b.Id,
                Description = b.Description,
                ContactPhone = b.ContactPhone,
                ContactEmail = b.ContactEmail,
                InstagramUrl = b.InstagramUrl,
                FacebookUrl = b.FacebookUrl,
                TwitterUrl = b.TwitterUrl,
                LogoId = b.LogoId
            });
        }

        public async Task<BioResponse> GetByIdAsync(Guid id)
        {
            var bio = await _readRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(
                    $"Bio with ID '{id}' was not found.");

            return new BioResponse
            {
                Id = bio.Id,
                Description = bio.Description,
                ContactPhone = bio.ContactPhone,
                ContactEmail = bio.ContactEmail,
                InstagramUrl = bio.InstagramUrl,
                FacebookUrl = bio.FacebookUrl,
                TwitterUrl = bio.TwitterUrl,
                LogoId = bio.LogoId
            };
        }

        public async Task CreateAsync(CreateBioRequest request)
        {
            FilmImage? newLogo = null;

            if (request.LogoPhoto != null)
            {
                string photoUrl = await _fileService.UploadAsync("images/logos", request.LogoPhoto);
                newLogo = new FilmImage { PhotoUrl = photoUrl };
            }

            var bio = new Bio
            {
                Description = request.Description,
                ContactPhone = request.ContactPhone,
                ContactEmail = request.ContactEmail,
                InstagramUrl = request.InstagramUrl,
                FacebookUrl = request.FacebookUrl,
                TwitterUrl = request.TwitterUrl,
                Logo = newLogo
            };

            await _writeRepository.AddAsync(bio);
            await _writeRepository.SaveChangeAsync();
        }

        public async Task UpdateAsync(Guid id, UpdateBioRequest request)
        {
            var bio = await _readRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(
                    $"Bio with ID '{id}' was not found.");

            bio.Description = request.Description;
            bio.ContactPhone = request.ContactPhone;
            bio.ContactEmail = request.ContactEmail;
            bio.InstagramUrl = request.InstagramUrl;
            bio.FacebookUrl = request.FacebookUrl;
            bio.TwitterUrl = request.TwitterUrl;
        

            if (request.LogoPhoto != null)
            {
                string photoUrl = await _fileService.UploadAsync("images/logos", request.LogoPhoto);
                bio.Logo = new FilmImage { PhotoUrl = photoUrl };
            }

            _writeRepository.Update(bio);
            await _writeRepository.SaveChangeAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            var bio = await _readRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(
                    $"Bio with ID '{id}' was not found.");

            _writeRepository.Delete(bio);
            await _writeRepository.SaveChangeAsync();

        }
    }
}
