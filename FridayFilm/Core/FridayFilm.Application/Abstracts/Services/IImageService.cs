using FridayFilm.Application.Dtos.ImageDtos;
using FridayFilm.Application.Pagination;
using FridayFilm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Application.Abstracts.Services;

public interface IImageService
{
    Task<PaginatedResponse<ImageResponse>> GetAllPaginatedAsync(PaginationRequest request, ImageTypeFilter? type);
}
