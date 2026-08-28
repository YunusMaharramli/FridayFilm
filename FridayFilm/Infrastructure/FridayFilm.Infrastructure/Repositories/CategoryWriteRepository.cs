using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Domain.Entities;
using FridayFilm.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Infrastructure.Repositories;

public class CategoryWriteRepository :
    WriteRepository<Category>, ICategoryWriteRepository
{
    public CategoryWriteRepository(FridayFilmDbContext context) : base(context)
    {
    }
}
