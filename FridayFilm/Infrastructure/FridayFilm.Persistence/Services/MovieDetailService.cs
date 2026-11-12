using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Dtos.MovieDetailDtos;
using FridayFilm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridayFilm.Application.Services
{
    public class MovieDetailService : IMovieDetailService
    {
        private readonly IMovieDetailReadRepository _readRepository;
        private readonly IMovieDetailWriteRepository _writeRepository;

        public MovieDetailService(IMovieDetailReadRepository readRepository, IMovieDetailWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<IEnumerable<MovieDetailResponse>> GetAllAsync()
        {
            var details = await _readRepository.GetAllAsync();

            return details.Select(d => new MovieDetailResponse
            {
                Id = d.Id,
                Description = d.Description,
                TrailerUrl = d.TrailerUrl
            });
        }

        public async Task<MovieDetailResponse?> GetByIdAsync(Guid id)
        {
            var detail = await _readRepository.GetByIdAsync(id);
            if (detail == null) return null;

            return new MovieDetailResponse
            {
                Id = detail.Id,
                Description = detail.Description,
                TrailerUrl = detail.TrailerUrl
            };
        }

        public async Task CreateAsync(CreateMovieDetailRequest request)
        {
            var detail = new MovieDetail
            {
                Description = request.Description,
                TrailerUrl = request.TrailerUrl
            };

            await _writeRepository.AddAsync(detail);
            await _writeRepository.SaveChangeAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateMovieDetailRequest request)
        {
            var detail = await _readRepository.GetByIdAsync(id);
            if (detail == null) return false;

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                detail.Description = request.Description;
            }

            if (!string.IsNullOrWhiteSpace(request.TrailerUrl))
            {
                detail.TrailerUrl = request.TrailerUrl;
            }

            _writeRepository.Update(detail);
            await _writeRepository.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var detail = await _readRepository.GetByIdAsync(id);
            if (detail == null) return false;

            _writeRepository.Delete(detail);
            await _writeRepository.SaveChangeAsync();

            return true;
        }
    }
}