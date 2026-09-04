using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Application.Dtos.ImageDtos
{
    public class ImageResponse
    {
        public Guid Id { get; set; }
        public string PhotoUrl { get; set; }
        public string SourceType { get; set; }
    }
}
