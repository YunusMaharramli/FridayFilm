using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Domain.Entities;
using FridayFilm.Infrastructure.Repositories;
using FridayFilm.Persistence.Contexts; // Öz DbContext qovluğuna uyğunlaşdır

namespace FridayFilm.Persistence.Repositories;

public class BioReadRepository : ReadRepository<Bio>, IBioReadRepository
{
    public BioReadRepository(FridayFilmDbContext context) : base(context) // DbContext adını özününkü ilə eyniləşdir
    {
    }
}