using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Domain.Entities;
using FridayFilm.Infrastructure.Repositories;
using FridayFilm.Persistence.Contexts;

namespace FridayFilm.Persistence.Repositories;

public class BioWriteRepository : WriteRepository<Bio>, IBioWriteRepository
{
    public BioWriteRepository(FridayFilmDbContext context) : base(context)
    {
    }
}