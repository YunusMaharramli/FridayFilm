using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Application.Dtos.MovieDetailDtos;

public class CreateMovieDetailRequest
{
    public string Description { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
}
