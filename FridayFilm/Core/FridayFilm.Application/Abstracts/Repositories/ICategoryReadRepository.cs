using FridayFilm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Application.Abstracts.Repositories;

public interface ICategoryReadRepository :
    IReadRepository<Category>
{
}
