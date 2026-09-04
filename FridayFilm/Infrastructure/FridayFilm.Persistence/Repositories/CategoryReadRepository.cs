using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Infrastructure.Repositories;

public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
{
    public CategoryReadRepository(FridayFilmDbContext context) : base(context)
    {
    }
}
