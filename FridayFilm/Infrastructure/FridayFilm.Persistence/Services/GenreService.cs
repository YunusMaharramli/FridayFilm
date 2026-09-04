using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Dtos.GenreDtos;
using FridayFilm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Persistence.Services;

public class GenreService : IGenreService
{
    private readonly IGenreReadRepository _readRepository;
    private readonly IGenreWriteRepository _writeRepository;

    public GenreService(IGenreReadRepository readRepository, IGenreWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IEnumerable<GenreResponse>> GetAllAsync()
    {
        var genres = await _readRepository.GetAllAsync();

        return genres.Select(g => new GenreResponse
        {
            Id = g.Id,
            Name = g.Name
        });
    }

    public async Task<GenreResponse?> GetByIdAsync(Guid id)
    {
        var genre = await _readRepository.GetByIdAsync(id);
        if (genre == null) return null;

        return new GenreResponse
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    public async Task CreateAsync(CreateGenreRequest request)
    {
        var genre = new Genre
        {
            Name = request.Name
        };

        await _writeRepository.AddAsync(genre);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateGenreRequest request)
    {
        var genre = await _readRepository.GetByIdAsync(id);
        if (genre == null) return false;

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            genre.Name = request.Name;
        }

        _writeRepository.Update(genre);
        await _writeRepository.SaveChangeAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var genre = await _readRepository.GetByIdAsync(id);
        if (genre == null) return false;

        _writeRepository.Delete(genre);
        await _writeRepository.SaveChangeAsync();

        return true;
    }
}
