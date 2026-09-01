using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Infrastructure.Repositories;
using FridayFilm.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Persistence.Repositories;

public class DirectorWriteRepository : WriteRepository<Director>, IDirectorWriteRepository
{
    public DirectorWriteRepository(FridayFilmDbContext context) : base(context)
    {
    }
}
