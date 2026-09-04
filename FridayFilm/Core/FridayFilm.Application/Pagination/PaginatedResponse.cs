namespace FridayFilm.Application.Pagination; // Qovluğun yeni adına uyğun namespace

public class PaginatedResponse<T>
{
    public IEnumerable<T> Data { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

    public PaginatedResponse(IEnumerable<T> data, int totalCount, int currentPage, int pageSize)
    {
        Data = data;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;

        // Riyazi olaraq ümumi səhifə sayının avtomatik tapılması
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}