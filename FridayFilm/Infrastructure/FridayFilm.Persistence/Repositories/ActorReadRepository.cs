using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Contexts;

namespace FridayFilm.Infrastructure.Repositories;

public class ActorReadRepository : ReadRepository<Actor>, IActorReadRepository
{
    public ActorReadRepository(FridayFilmDbContext context) : base(context)
    {
    }
}