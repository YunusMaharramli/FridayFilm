namespace FridayFilm.Application.Dtos.CategoryDtos;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}