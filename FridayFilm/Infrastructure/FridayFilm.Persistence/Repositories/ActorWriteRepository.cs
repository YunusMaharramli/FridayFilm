using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Contexts;

namespace FridayFilm.Infrastructure.Repositories;

public class ActorWriteRepository : WriteRepository<Actor>, IActorWriteRepository
{
    public ActorWriteRepository(FridayFilmDbContext context) : base(context)
    {
    }
}