using System.Text.Json;
using FridayFilm.Application.Dtos.MovieDtos;
using FridayFilm.Application.Exceptions;
using FridayFilm.Application.Validators.Movies;
using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Contexts;
using FridayFilm.WebApi.ExceptionHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace FridayFilm.Tests;

public class RegressionTests
{
    [Theory]
    [InlineData(0, 20)]
    [InlineData(-1, 20)]
    [InlineData(1, 0)]
    [InlineData(int.MaxValue, 100)]
    public void InvalidPaginationIsRejected(int page, int size)
    {
        var result = new MovieQueryRequestValidator().Validate(new MovieQueryRequest { Page = page, Size = size });
        Assert.False(result.IsValid);
    }

    [Fact]
    public void ValidMovieAndQueryAreAccepted()
    {
        Assert.True(new MovieQueryRequestValidator().Validate(new MovieQueryRequest { Page = 2, Size = 20, Search = "Film", Sort = "rating" }).IsValid);
        var movie = ValidMovie();
        Assert.True(new CreateMovieRequestValidator().Validate(movie).IsValid);
        movie.IMDB = 8.55m;
        Assert.False(new CreateMovieRequestValidator().Validate(movie).IsValid);
    }

    [Fact]
    public void MissingDetailAndNullRelationsAreRejectedWithoutCrashing()
    {
        var movie = ValidMovie();
        movie.MovieDetail = null!;
        movie.GenreIds = null!;
        Assert.False(new CreateMovieRequestValidator().Validate(movie).IsValid);
    }

    [Fact]
    public void SoftDeletedBiosAreFilteredInSql()
    {
        using var db = new FridayFilmDbContext(new DbContextOptionsBuilder<FridayFilmDbContext>()
            .UseNpgsql("Host=localhost;Database=not_used;Username=not_used;Password=not_used").Options);
        Assert.Contains("IsDeleted", db.Bios.ToQueryString());
        Assert.NotEmpty(db.Model.FindEntityType(typeof(Bio))!.GetDeclaredQueryFilters());
    }

    [Theory]
    [InlineData(400)]
    [InlineData(401)]
    [InlineData(403)]
    [InlineData(404)]
    [InlineData(409)]
    [InlineData(500)]
    public async Task ExceptionsHaveConsistentStatusAndDoNotLeakInternalErrors(int status)
    {
        Exception exception = status switch
        {
            400 => new ValidationException("validation"),
            401 => new UnauthorizedException("unauthorized"),
            403 => new ForbiddenException("forbidden"),
            404 => new NotFoundException("not found"),
            409 => new ConflictException("conflict"),
            _ => new InvalidOperationException("internal secret must not leak")
        };
        var http = new DefaultHttpContext();
        http.Response.Body = new MemoryStream();
        var middleware = new GlobalExceptionHandler(_ => throw exception, NullLogger<GlobalExceptionHandler>.Instance);
        await middleware.InvokeAsync(http);
        Assert.Equal(status, http.Response.StatusCode);
        http.Response.Body.Position = 0;
        var payload = await JsonDocument.ParseAsync(http.Response.Body);
        Assert.Equal(status, payload.RootElement.GetProperty("statusCode").GetInt32());
        Assert.True(payload.RootElement.TryGetProperty("traceId", out _));
        Assert.DoesNotContain("internal secret", payload.RootElement.GetRawText());
    }

    private static CreateMovieRequest ValidMovie() => new()
    {
        Name = "Regression movie", IMDB = 8.5m, Year = 2026,
        CoverImg = "https://example.com/poster.jpg", Duration = TimeSpan.FromMinutes(90),
        CategoryId = Guid.NewGuid(), LanguageId = Guid.NewGuid(),
        MovieDetail = new() { Description = "A test movie", TrailerUrl = "https://example.com/trailer" }
    };
}
