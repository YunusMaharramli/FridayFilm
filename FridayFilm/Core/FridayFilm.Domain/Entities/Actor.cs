using FridayFilm.Domain.Common;
using FridayFilm.Domain.Enums;

namespace FridayFilm.Domain.Entities
{
    public class Actor : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string? Nickname { get; set; }
        public string? Bio { get; set; }
    }
}