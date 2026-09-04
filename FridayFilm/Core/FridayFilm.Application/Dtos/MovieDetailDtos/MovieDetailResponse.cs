using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Application.Dtos.MovieDetailDtos;

public class MovieDetailResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string TrailerUrl { get; set; }
}
