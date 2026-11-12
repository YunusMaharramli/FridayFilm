using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FridayFilm.Application.DTOs.BioDtos;

namespace FridayFilm.Application.Abstracts.Services;

public interface IBioService
{
    Task<IEnumerable<BioResponse>> GetAllAsync();
    Task<BioResponse> GetByIdAsync(Guid id);
    Task CreateAsync(CreateBioRequest request);
    Task UpdateAsync(Guid id, UpdateBioRequest request);
    Task DeleteAsync(Guid id);
}
