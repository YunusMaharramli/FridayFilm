using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Dtos.ImageDtos;
using FridayFilm.Application.Pagination;
using FridayFilm.Domain.Entities;
using FridayFilm.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FridayFilm.Application.Services;

public class ImageService : IImageService
{
    private readonly IFilmImageReadRepository _readRepository;

    public ImageService(IFilmImageReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<PaginatedResponse<ImageResponse>> GetAllPaginatedAsync(PaginationRequest request, ImageTypeFilter? type)
    {
        // 1. Baza sorğusunu başladırıq (hələ bazaya getmir, sadəcə Query qurulur)
        var query = _readRepository.Query();

        // 2. Dinamik Include və Şərtləri qururuq
        if (type.HasValue)
        {
            switch (type.Value)
            {
                case ImageTypeFilter.Movie:
                    query = query.Where(x => x.MovieId != null);
                    break;
                case ImageTypeFilter.Actor:
                    query = query.Include(x => x.Actor).Where(x => x.Actor != null);
                    break;
                case ImageTypeFilter.Director:
                    query = query.Include(x => x.Director).Where(x => x.Director != null);
                    break;
                case ImageTypeFilter.Bio:
                    query = query.Include(x => x.Bio).Where(x => x.Bio != null);
                    break;
            }
        }
        else
        {
            // Əgər filter yoxdursa, şəklin tipini təyin edə bilmək üçün hamısını çəkirik
            query = query.Include(x => x.Actor)
                         .Include(x => x.Director)
                         .Include(x => x.Bio);
        }

        // 3. Ümumi sayı tapırıq (Pagination üçün)
        int totalCount = await query.CountAsync();

        // 4. Səhifələməni (Skip/Take) tətbiq edib məlumatı yaddaşa çəkirik
        int skip = (request.Page - 1) * request.Size;
        var images = await query.Skip(skip).Take(request.Size).ToListAsync();

        // 5. Şəkilləri DTO-ya map edirik
        var mappedData = images.Select(img => new ImageResponse
        {
            Id = img.Id,
            PhotoUrl = img.PhotoUrl,
            SourceType = DetermineSource(img)
        }).ToList();

        return new PaginatedResponse<ImageResponse>(mappedData, totalCount, request.Page, request.Size);
    }
    private string DetermineSource(FilmImage img)
    {
        if (img.MovieId != null) return "Movie";
        if (img.Actor != null) return "Actor";
        if (img.Director != null) return "Director";
        if (img.Bio != null) return "Bio";
        return "Uncategorized";
    }
}