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

            // Boş gəlməyibsə köhnəni əzib yenisini yazırıq
            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                bio.Description = request.Description;
            }

            if (!string.IsNullOrWhiteSpace(request.ContactPhone))
            {
                bio.ContactPhone = request.ContactPhone;
            }

            if (!string.IsNullOrWhiteSpace(request.ContactEmail))
            {
                bio.ContactEmail = request.ContactEmail;
            }

            if (!string.IsNullOrWhiteSpace(request.InstagramUrl))
            {
                bio.InstagramUrl = request.InstagramUrl;
            }

            if (!string.IsNullOrWhiteSpace(request.FacebookUrl))
            {
                bio.FacebookUrl = request.FacebookUrl;
            }

            if (!string.IsNullOrWhiteSpace(request.TwitterUrl))
            {
                bio.TwitterUrl = request.TwitterUrl;
            }

            // Şəkil (Loqo) yenilənməsi prosesi
            if (request.LogoPhoto != null)
            {
                if (bio.Logo != null && !string.IsNullOrEmpty(bio.Logo.PhotoUrl))
                {
                    _fileService.Delete(bio.Logo.PhotoUrl);
                }

                string photoUrl = await _fileService.UploadAsync("images/logos", request.LogoPhoto);

                if (bio.Logo != null)
                {
                    bio.Logo.PhotoUrl = photoUrl;
                }
                else
                {
                    bio.Logo = new FilmImage { PhotoUrl = photoUrl };
                }
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
