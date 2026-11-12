namespace FridayFilm.Application.Pagination;

public class PaginationRequest
{
   
    public int Page { get; set; } = 1;

    private int _size = 10;
    public int Size
    {
        get => _size;
        set => _size = value > 10 ? 10 : value; 
    }
}