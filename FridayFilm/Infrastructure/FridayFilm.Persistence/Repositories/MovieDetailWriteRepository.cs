using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Domain.Entities;
using FridayFilm.Infrastructure.Repositories;
using FridayFilm.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Persistence.Repositories;

public class MovieDetailWriteRepository : WriteRepository<MovieDetail>, IMovieDetailWriteRepository
{
    public MovieDetailWriteRepository(FridayFilmDbContext context) : base(context)
    {
    }
}
