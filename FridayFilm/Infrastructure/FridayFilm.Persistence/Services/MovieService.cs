using FluentValidation;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Dtos.MovieDtos;
using FridayFilm.Application.Exceptions;
using FridayFilm.Domain.Common;
using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using FridayFilm.Application.Pagination;
using FridayFilm.Application.Dtos.MovieDetailDtos;

namespace FridayFilm.Persistence.Services;

public sealed class MovieService : IMovieService
{
    private readonly FridayFilmDbContext _context;
    private readonly IValidator<CreateMovieRequest> _validator;

    public MovieService(FridayFilmDbContext context, IValidator<CreateMovieRequest> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Guid> CreateAsync(CreateMovieRequest request, CancellationToken cancellationToken = default)
    {
        var validation = await _validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            throw new Application.Exceptions.ValidationException(
                string.Join(" ", validation.Errors.Select(x => x.ErrorMessage)));
        }

        if (!await _context.Categories.AnyAsync(x => x.Id == request.CategoryId && !x.IsDeleted, cancellationToken))
            throw new NotFoundException("Kateqoriya tapılmadı.");

        if (!await _context.Languages.AnyAsync(x => x.Id == request.LanguageId && !x.IsDeleted, cancellationToken))
            throw new NotFoundException("Dil tapılmadı.");

        var genres = await LoadRelationsAsync<Genre>(request.GenreIds, "Janr", cancellationToken);
        var directors = await LoadRelationsAsync<Director>(request.DirectorIds, "Rejissor", cancellationToken);
        var actors = await LoadRelationsAsync<Actor>(request.ActorIds, "Aktyor", cancellationToken);

        var id = Guid.NewGuid();
        var movie = new Movie
        {
            Id = id,
            Name = request.Name.Trim(),
            IMDB = request.IMDB,
            Year = request.Year,
            CoverImg = request.CoverImg.Trim(),
            Duration = request.Duration,
            RateCount = 0,
            LanguageId = request.LanguageId,
            CategoryId = request.CategoryId,
            Genres = genres,
            Directors = directors,
            Actors = actors,
            MovieDetail = new MovieDetail
            {
                Id = id,
                Description = request.MovieDetail.Description.Trim(),
                TrailerUrl = request.MovieDetail.TrailerUrl.Trim()
            }
        };

        // Existing related entities remain tracked as Unchanged; only the new graph is inserted.
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync(cancellationToken);
        return movie.Id;
    }

    public async Task<PaginatedResponse<MovieResponse>> GetAllAsync(PaginationRequest request, CancellationToken cancellationToken = default)
    {
        var offset = ((long)request.Page - 1) * request.Size;
        if (request.Page < 1 || request.Size < 1 || offset > int.MaxValue)
            throw new Application.Exceptions.ValidationException("Page və size düzgün müsbət qiymətlər olmalıdır.");

        var count = await _context.Movies.CountAsync(cancellationToken);
        var movies = await MovieQuery().AsNoTracking()
            .OrderByDescending(x => x.CreatedDate).ThenBy(x => x.Id)
            .Skip((int)offset).Take(request.Size).ToListAsync(cancellationToken);
        return new PaginatedResponse<MovieResponse>(movies.Select(Map), count, request.Page, request.Size);
    }

    public async Task<MovieResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var movie = await MovieQuery().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new NotFoundException("Film tapılmadı.");
        return Map(movie);
    }

    public async Task UpdateAsync(Guid id, UpdateMovieRequest request, CancellationToken cancellationToken = default)
    {
        var validation = await _validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new Application.Exceptions.ValidationException(string.Join(" ", validation.Errors.Select(x => x.ErrorMessage)));

        var movie = await MovieQuery().SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new NotFoundException("Film tapılmadı.");

        if (!await _context.Categories.AnyAsync(x => x.Id == request.CategoryId && !x.IsDeleted, cancellationToken))
            throw new NotFoundException("Kateqoriya tapılmadı.");
        if (!await _context.Languages.AnyAsync(x => x.Id == request.LanguageId && !x.IsDeleted, cancellationToken))
            throw new NotFoundException("Dil tapılmadı.");

        var genres = await LoadRelationsAsync<Genre>(request.GenreIds, "Janr", cancellationToken);
        var directors = await LoadRelationsAsync<Director>(request.DirectorIds, "Rejissor", cancellationToken);
        var actors = await LoadRelationsAsync<Actor>(request.ActorIds, "Aktyor", cancellationToken);

        // A separately soft-deleted detail still occupies the shared primary key.
        var detail = await _context.MovieDetails.IgnoreQueryFilters()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (detail is null)
        {
            detail = new MovieDetail { Id = id, Movie = movie };
            _context.MovieDetails.Add(detail);
        }
        detail.IsDeleted = false;
        detail.Description = request.MovieDetail.Description.Trim();
        detail.TrailerUrl = request.MovieDetail.TrailerUrl.Trim();
        movie.MovieDetail = detail;
        movie.Name = request.Name.Trim();
        movie.IMDB = request.IMDB;
        movie.Year = request.Year;
        movie.CoverImg = request.CoverImg.Trim();
        movie.Duration = request.Duration;
        movie.LanguageId = request.LanguageId;
        movie.CategoryId = request.CategoryId;
        movie.Genres = genres;
        movie.Directors = directors;
        movie.Actors = actors;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var movie = await _context.Movies.Include(x => x.MovieDetail).Include(x => x.Images)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new NotFoundException("Film tapılmadı.");
        movie.IsDeleted = true;
        if (movie.MovieDetail is not null)
            movie.MovieDetail.IsDeleted = true;
        foreach (var image in movie.Images)
            image.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<Movie> MovieQuery() => _context.Movies
        .Include(x => x.MovieDetail).Include(x => x.Genres)
        .Include(x => x.Directors).Include(x => x.Actors).AsSplitQuery();

    private static MovieResponse Map(Movie movie) => new()
    {
        Id = movie.Id, Name = movie.Name, IMDB = movie.IMDB, Year = movie.Year,
        CoverImg = movie.CoverImg, Duration = movie.Duration, RateCount = movie.RateCount,
        LanguageId = movie.LanguageId, CategoryId = movie.CategoryId,
        GenreIds = movie.Genres.Select(x => x.Id).ToList(),
        DirectorIds = movie.Directors.Select(x => x.Id).ToList(),
        ActorIds = movie.Actors.Select(x => x.Id).ToList(),
        MovieDetail = movie.MovieDetail is null ? null : new MovieDetailResponse
        {
            Id = movie.MovieDetail.Id,
            Description = movie.MovieDetail.Description,
            TrailerUrl = movie.MovieDetail.TrailerUrl
        }
    };

    private async Task<List<TEntity>> LoadRelationsAsync<TEntity>(
        IEnumerable<Guid> requestedIds, string entityName, CancellationToken cancellationToken)
        where TEntity : BaseEntity
    {
        var ids = requestedIds.Distinct().ToArray();
        var entities = await _context.Set<TEntity>()
            .Where(x => ids.Contains(x.Id) && !x.IsDeleted)
            .ToListAsync(cancellationToken);

        var missingIds = ids.Except(entities.Select(x => x.Id)).ToArray();
        if (missingIds.Length > 0)
            throw new NotFoundException($"{entityName} tapılmadı: {string.Join(", ", missingIds)}");

        return entities;
    }
}
