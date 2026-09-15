using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAllData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Bio", "CreatedDate", "FullName", "Gender", "ImageId", "IsDeleted", "Nationality", "Slug", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("22222222-3333-4444-5555-666666666604"), "Co-creator of The Matrix, a landmark of science fiction cinema.", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Lana Wachowski", 2, null, false, "American", "lana-wachowski", null },
                    { new Guid("22222222-3333-4444-5555-666666666605"), "Creator of the Mad Max franchise, known for practical action filmmaking.", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "George Miller", 1, null, false, "Australian", "george-miller", null },
                    { new Guid("22222222-3333-4444-5555-666666666606"), "Known for psychologically intense films such as Black Swan and Requiem for a Dream.", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Darren Aronofsky", 1, null, false, "American", "darren-aronofsky", null },
                    { new Guid("22222222-3333-4444-5555-666666666607"), "One of the most influential directors in cinema history, famous for crime dramas.", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Martin Scorsese", 1, null, false, "American", "martin-scorsese", null },
                    { new Guid("22222222-3333-4444-5555-666666666608"), "Acclaimed for atmospheric science fiction including Arrival, Blade Runner 2049 and Dune.", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Denis Villeneuve", 1, null, false, "Canadian", "denis-villeneuve", null }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("bbbb0000-0000-0000-0000-000000000001"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Action", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000002"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Adventure", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000003"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Science Fiction", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000004"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Drama", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000005"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Thriller", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000006"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Crime", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000007"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Comedy", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000008"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Romance", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000009"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Mystery", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000010"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Fantasy", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000011"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Historical", null },
                    { new Guid("bbbb0000-0000-0000-0000-000000000012"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, "Biography", null }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Lang", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("aaaa0000-0000-0000-0000-000000000001"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, 1, null },
                    { new Guid("aaaa0000-0000-0000-0000-000000000002"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, 2, null },
                    { new Guid("aaaa0000-0000-0000-0000-000000000003"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, 3, null },
                    { new Guid("aaaa0000-0000-0000-0000-000000000004"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, 4, null }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CategoryId", "CoverImg", "CreatedDate", "Duration", "IMDB", "IsDeleted", "LanguageId", "Name", "RateCount", "UpdatedDate", "Year" },
                values: new object[,]
                {
                    { new Guid("cccc0000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), "/images/movies/inception.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 2, 28, 0, 0), 8.8m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "Inception", 2400000, null, 2010 },
                    { new Guid("cccc0000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/movies/the-dark-knight.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 2, 32, 0, 0), 9.0m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "The Dark Knight", 2900000, null, 2008 },
                    { new Guid("cccc0000-0000-0000-0000-000000000003"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "/images/movies/oppenheimer.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 3, 0, 0, 0), 8.3m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "Oppenheimer", 820000, null, 2023 },
                    { new Guid("cccc0000-0000-0000-0000-000000000004"), new Guid("55555555-5555-5555-5555-555555555555"), "/images/movies/interstellar.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 2, 49, 0, 0), 8.7m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "Interstellar", 2100000, null, 2014 },
                    { new Guid("cccc0000-0000-0000-0000-000000000005"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "/images/movies/pulp-fiction.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 2, 34, 0, 0), 8.9m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "Pulp Fiction", 2200000, null, 1994 },
                    { new Guid("cccc0000-0000-0000-0000-000000000006"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "/images/movies/django-unchained.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 2, 45, 0, 0), 8.4m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "Django Unchained", 1700000, null, 2012 },
                    { new Guid("cccc0000-0000-0000-0000-000000000007"), new Guid("22222222-2222-2222-2222-222222222222"), "/images/movies/barbie.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 1, 54, 0, 0), 6.8m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "Barbie", 580000, null, 2023 },
                    { new Guid("cccc0000-0000-0000-0000-000000000008"), new Guid("33333333-3333-3333-3333-333333333333"), "/images/movies/little-women.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 2, 15, 0, 0), 7.8m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "Little Women", 230000, null, 2019 },
                    { new Guid("cccc0000-0000-0000-0000-000000000009"), new Guid("55555555-5555-5555-5555-555555555555"), "/images/movies/the-matrix.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 2, 16, 0, 0), 8.7m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "The Matrix", 2000000, null, 1999 },
                    { new Guid("cccc0000-0000-0000-0000-000000000010"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/movies/mad-max-fury-road.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 2, 0, 0, 0), 8.1m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "Mad Max: Fury Road", 1100000, null, 2015 },
                    { new Guid("cccc0000-0000-0000-0000-000000000011"), new Guid("77777777-7777-7777-7777-777777777777"), "/images/movies/black-swan.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 1, 48, 0, 0), 8.0m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "Black Swan", 750000, null, 2010 },
                    { new Guid("cccc0000-0000-0000-0000-000000000012"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "/images/movies/the-wolf-of-wall-street.jpg", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 3, 0, 0, 0), 8.2m, false, new Guid("aaaa0000-0000-0000-0000-000000000002"), "The Wolf of Wall Street", 1500000, null, 2013 }
                });

            migrationBuilder.InsertData(
                table: "ActorMovie",
                columns: new[] { "ActorsId", "MoviesId" },
                values: new object[,]
                {
                    { new Guid("11111111-2222-3333-4444-555555555501"), new Guid("cccc0000-0000-0000-0000-000000000001") },
                    { new Guid("11111111-2222-3333-4444-555555555501"), new Guid("cccc0000-0000-0000-0000-000000000006") },
                    { new Guid("11111111-2222-3333-4444-555555555501"), new Guid("cccc0000-0000-0000-0000-000000000012") },
                    { new Guid("11111111-2222-3333-4444-555555555503"), new Guid("cccc0000-0000-0000-0000-000000000001") },
                    { new Guid("11111111-2222-3333-4444-555555555503"), new Guid("cccc0000-0000-0000-0000-000000000002") },
                    { new Guid("11111111-2222-3333-4444-555555555503"), new Guid("cccc0000-0000-0000-0000-000000000003") },
                    { new Guid("11111111-2222-3333-4444-555555555504"), new Guid("cccc0000-0000-0000-0000-000000000007") },
                    { new Guid("11111111-2222-3333-4444-555555555504"), new Guid("cccc0000-0000-0000-0000-000000000012") },
                    { new Guid("11111111-2222-3333-4444-555555555505"), new Guid("cccc0000-0000-0000-0000-000000000001") },
                    { new Guid("11111111-2222-3333-4444-555555555505"), new Guid("cccc0000-0000-0000-0000-000000000010") },
                    { new Guid("11111111-2222-3333-4444-555555555506"), new Guid("cccc0000-0000-0000-0000-000000000008") },
                    { new Guid("11111111-2222-3333-4444-555555555507"), new Guid("cccc0000-0000-0000-0000-000000000009") },
                    { new Guid("11111111-2222-3333-4444-555555555508"), new Guid("cccc0000-0000-0000-0000-000000000011") },
                    { new Guid("11111111-2222-3333-4444-555555555509"), new Guid("cccc0000-0000-0000-0000-000000000002") },
                    { new Guid("11111111-2222-3333-4444-555555555510"), new Guid("cccc0000-0000-0000-0000-000000000010") }
                });

            migrationBuilder.InsertData(
                table: "DirectorMovie",
                columns: new[] { "DirectorsId", "MoviesId" },
                values: new object[,]
                {
                    { new Guid("22222222-3333-4444-5555-666666666601"), new Guid("cccc0000-0000-0000-0000-000000000001") },
                    { new Guid("22222222-3333-4444-5555-666666666601"), new Guid("cccc0000-0000-0000-0000-000000000002") },
                    { new Guid("22222222-3333-4444-5555-666666666601"), new Guid("cccc0000-0000-0000-0000-000000000003") },
                    { new Guid("22222222-3333-4444-5555-666666666601"), new Guid("cccc0000-0000-0000-0000-000000000004") },
                    { new Guid("22222222-3333-4444-5555-666666666602"), new Guid("cccc0000-0000-0000-0000-000000000005") },
                    { new Guid("22222222-3333-4444-5555-666666666602"), new Guid("cccc0000-0000-0000-0000-000000000006") },
                    { new Guid("22222222-3333-4444-5555-666666666603"), new Guid("cccc0000-0000-0000-0000-000000000007") },
                    { new Guid("22222222-3333-4444-5555-666666666603"), new Guid("cccc0000-0000-0000-0000-000000000008") },
                    { new Guid("22222222-3333-4444-5555-666666666604"), new Guid("cccc0000-0000-0000-0000-000000000009") },
                    { new Guid("22222222-3333-4444-5555-666666666605"), new Guid("cccc0000-0000-0000-0000-000000000010") },
                    { new Guid("22222222-3333-4444-5555-666666666606"), new Guid("cccc0000-0000-0000-0000-000000000011") },
                    { new Guid("22222222-3333-4444-5555-666666666607"), new Guid("cccc0000-0000-0000-0000-000000000012") }
                });

            migrationBuilder.InsertData(
                table: "FilmImages",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "MovieId", "PhotoUrl", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("dddd0000-0000-0000-0000-000000000001"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000001"), "/images/movies/inception-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000002"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000001"), "/images/movies/inception-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000003"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000002"), "/images/movies/the-dark-knight-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000004"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000002"), "/images/movies/the-dark-knight-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000005"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000003"), "/images/movies/oppenheimer-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000006"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000003"), "/images/movies/oppenheimer-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000007"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000004"), "/images/movies/interstellar-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000008"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000004"), "/images/movies/interstellar-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000009"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000005"), "/images/movies/pulp-fiction-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000010"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000005"), "/images/movies/pulp-fiction-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000011"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000006"), "/images/movies/django-unchained-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000012"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000006"), "/images/movies/django-unchained-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000013"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000007"), "/images/movies/barbie-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000014"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000007"), "/images/movies/barbie-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000015"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000008"), "/images/movies/little-women-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000016"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000008"), "/images/movies/little-women-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000017"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000009"), "/images/movies/the-matrix-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000018"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000009"), "/images/movies/the-matrix-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000019"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000010"), "/images/movies/mad-max-fury-road-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000020"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000010"), "/images/movies/mad-max-fury-road-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000021"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000011"), "/images/movies/black-swan-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000022"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000011"), "/images/movies/black-swan-2.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000023"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000012"), "/images/movies/the-wolf-of-wall-street-1.jpg", null },
                    { new Guid("dddd0000-0000-0000-0000-000000000024"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("cccc0000-0000-0000-0000-000000000012"), "/images/movies/the-wolf-of-wall-street-2.jpg", null }
                });

            migrationBuilder.InsertData(
                table: "GenreMovie",
                columns: new[] { "GenresId", "MoviesId" },
                values: new object[,]
                {
                    { new Guid("bbbb0000-0000-0000-0000-000000000001"), new Guid("cccc0000-0000-0000-0000-000000000001") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000001"), new Guid("cccc0000-0000-0000-0000-000000000002") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000001"), new Guid("cccc0000-0000-0000-0000-000000000009") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000001"), new Guid("cccc0000-0000-0000-0000-000000000010") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000002"), new Guid("cccc0000-0000-0000-0000-000000000004") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000002"), new Guid("cccc0000-0000-0000-0000-000000000006") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000002"), new Guid("cccc0000-0000-0000-0000-000000000007") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000002"), new Guid("cccc0000-0000-0000-0000-000000000010") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000003"), new Guid("cccc0000-0000-0000-0000-000000000001") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000003"), new Guid("cccc0000-0000-0000-0000-000000000004") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000003"), new Guid("cccc0000-0000-0000-0000-000000000009") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000003"), new Guid("cccc0000-0000-0000-0000-000000000010") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000002") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000003") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000004") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000005") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000006") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000008") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000011") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000012") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000005"), new Guid("cccc0000-0000-0000-0000-000000000001") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000005"), new Guid("cccc0000-0000-0000-0000-000000000011") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000006"), new Guid("cccc0000-0000-0000-0000-000000000002") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000006"), new Guid("cccc0000-0000-0000-0000-000000000005") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000006"), new Guid("cccc0000-0000-0000-0000-000000000006") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000006"), new Guid("cccc0000-0000-0000-0000-000000000012") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000007"), new Guid("cccc0000-0000-0000-0000-000000000007") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000007"), new Guid("cccc0000-0000-0000-0000-000000000012") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000008"), new Guid("cccc0000-0000-0000-0000-000000000008") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000009"), new Guid("cccc0000-0000-0000-0000-000000000011") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000010"), new Guid("cccc0000-0000-0000-0000-000000000007") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000011"), new Guid("cccc0000-0000-0000-0000-000000000003") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000012"), new Guid("cccc0000-0000-0000-0000-000000000003") },
                    { new Guid("bbbb0000-0000-0000-0000-000000000012"), new Guid("cccc0000-0000-0000-0000-000000000012") }
                });

            migrationBuilder.InsertData(
                table: "MovieDetails",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDeleted", "TrailerUrl", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("cccc0000-0000-0000-0000-000000000001"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Dom Cobb yuxu içində məlumat oğurlayan usta bir oğrudur. Ona son bir iş təklif olunur: oğurluq yox, fikir əkmək — inception. Ailəsinə qovuşmaq üçün komandası ilə şüurun ən dərin qatlarına enir.", false, "https://www.youtube.com/watch?v=YoHD9XEInc0", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000002"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Joker Qothem şəhərini xaosa sürükləyəndə Batman, Komissar Gordon və prokuror Harvey Dent birləşir. Lakin Jokerin əsl silahı zorakılıq deyil — insanların bir-birinə olan inamını qırmaqdır.", false, "https://www.youtube.com/watch?v=EXeTwQWrcwY", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000003"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "J. Robert Oppenheimer Manhattan layihəsinə rəhbərlik edərək atom bombasını yaradır. Zəfərdən sonra isə yaratdığı silahın nəticələri və vicdan əzabı onu izləməyə başlayır.", false, "https://www.youtube.com/watch?v=uYPbbksJxIg", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000004"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Yer üzü yaşayış üçün yararsızlaşdıqca bir qrup tədqiqatçı qurd dəliyindən keçərək bəşəriyyətə yeni ev axtarır. Zaman, cazibə və ata-övlad sevgisi arasında çətin seçim onları gözləyir.", false, "https://www.youtube.com/watch?v=zSWdZVtXT7E", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000005"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "İki muzdlu qatil, bir boksçu, cinayət aləminin patronu və onun arvadı — hekayələri gözlənilməz şəkildə kəsişir. Qeyri-xətti quruluşu ilə müasir kinonun gedişatını dəyişən əsər.", false, "https://www.youtube.com/watch?v=s7EdQ4FqbhY", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000006"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Azadlığa buraxılmış qul Django, mükafat ovçusu Dr. Schultz ilə birləşərək arvadını qəddar plantasiya sahibinin əlindən xilas etməyə çalışır.", false, "https://www.youtube.com/watch?v=0fUCuvNlOCg", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000007"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Barbie mükəmməl Barbieland dünyasını tərk edib real dünyaya çıxır. Kimlik, gözləntilər və özünü tapmaq haqqında rəngarəng və kinayəli bir səyahət başlayır.", false, "https://www.youtube.com/watch?v=pBk4NYhWNMM", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000008"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "March bacılarının böyümə hekayəsi — sevgi, itki, yaradıcılıq və müstəqillik arasında öz yollarını axtaran dörd qadının portreti.", false, "https://www.youtube.com/watch?v=AST2-4db4ic", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000009"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Proqramçı Neo yaşadığı dünyanın simulyasiya olduğunu öyrənir. Morpheus və Trinity ilə birlikdə maşınların qurduğu Matrisə qarşı üsyana qoşulur.", false, "https://www.youtube.com/watch?v=vKQi3bBA1y8", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000010"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Post-apokaliptik səhrada Max və Furiosa zalım Immortan Joe-dan qaçan qadınları xilas etmək üçün dayanmadan davam edən vəhşi bir təqib yarışına girir.", false, "https://www.youtube.com/watch?v=hEJnMQG9ev8", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000011"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Balerina Nina Qu Gölü tamaşasında baş rola seçilir. Mükəmməllik axtarışı və rəqabət onu tədricən öz şüurunun qaranlıq tərəfinə sürükləyir.", false, "https://www.youtube.com/watch?v=5jaI1XOB-bs", null },
                    { new Guid("cccc0000-0000-0000-0000-000000000012"), new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Jordan Belfort-un Wall Street-də sıfırdan sərvətə, oradan da süquta gedən yolu. Xəyanət, həddindən artıq israf və FBI təqibi ilə dolu həqiqi hekayə.", false, "https://www.youtube.com/watch?v=iszwuX1AK6A", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555501"), new Guid("cccc0000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555501"), new Guid("cccc0000-0000-0000-0000-000000000006") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555501"), new Guid("cccc0000-0000-0000-0000-000000000012") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555503"), new Guid("cccc0000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555503"), new Guid("cccc0000-0000-0000-0000-000000000002") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555503"), new Guid("cccc0000-0000-0000-0000-000000000003") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555504"), new Guid("cccc0000-0000-0000-0000-000000000007") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555504"), new Guid("cccc0000-0000-0000-0000-000000000012") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555505"), new Guid("cccc0000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555505"), new Guid("cccc0000-0000-0000-0000-000000000010") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555506"), new Guid("cccc0000-0000-0000-0000-000000000008") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555507"), new Guid("cccc0000-0000-0000-0000-000000000009") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555508"), new Guid("cccc0000-0000-0000-0000-000000000011") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555509"), new Guid("cccc0000-0000-0000-0000-000000000002") });

            migrationBuilder.DeleteData(
                table: "ActorMovie",
                keyColumns: new[] { "ActorsId", "MoviesId" },
                keyValues: new object[] { new Guid("11111111-2222-3333-4444-555555555510"), new Guid("cccc0000-0000-0000-0000-000000000010") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666601"), new Guid("cccc0000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666601"), new Guid("cccc0000-0000-0000-0000-000000000002") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666601"), new Guid("cccc0000-0000-0000-0000-000000000003") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666601"), new Guid("cccc0000-0000-0000-0000-000000000004") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666602"), new Guid("cccc0000-0000-0000-0000-000000000005") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666602"), new Guid("cccc0000-0000-0000-0000-000000000006") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666603"), new Guid("cccc0000-0000-0000-0000-000000000007") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666603"), new Guid("cccc0000-0000-0000-0000-000000000008") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666604"), new Guid("cccc0000-0000-0000-0000-000000000009") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666605"), new Guid("cccc0000-0000-0000-0000-000000000010") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666606"), new Guid("cccc0000-0000-0000-0000-000000000011") });

            migrationBuilder.DeleteData(
                table: "DirectorMovie",
                keyColumns: new[] { "DirectorsId", "MoviesId" },
                keyValues: new object[] { new Guid("22222222-3333-4444-5555-666666666607"), new Guid("cccc0000-0000-0000-0000-000000000012") });

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666608"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "FilmImages",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000001"), new Guid("cccc0000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000001"), new Guid("cccc0000-0000-0000-0000-000000000002") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000001"), new Guid("cccc0000-0000-0000-0000-000000000009") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000001"), new Guid("cccc0000-0000-0000-0000-000000000010") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000002"), new Guid("cccc0000-0000-0000-0000-000000000004") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000002"), new Guid("cccc0000-0000-0000-0000-000000000006") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000002"), new Guid("cccc0000-0000-0000-0000-000000000007") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000002"), new Guid("cccc0000-0000-0000-0000-000000000010") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000003"), new Guid("cccc0000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000003"), new Guid("cccc0000-0000-0000-0000-000000000004") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000003"), new Guid("cccc0000-0000-0000-0000-000000000009") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000003"), new Guid("cccc0000-0000-0000-0000-000000000010") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000002") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000003") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000004") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000005") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000006") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000008") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000011") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000004"), new Guid("cccc0000-0000-0000-0000-000000000012") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000005"), new Guid("cccc0000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000005"), new Guid("cccc0000-0000-0000-0000-000000000011") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000006"), new Guid("cccc0000-0000-0000-0000-000000000002") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000006"), new Guid("cccc0000-0000-0000-0000-000000000005") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000006"), new Guid("cccc0000-0000-0000-0000-000000000006") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000006"), new Guid("cccc0000-0000-0000-0000-000000000012") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000007"), new Guid("cccc0000-0000-0000-0000-000000000007") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000007"), new Guid("cccc0000-0000-0000-0000-000000000012") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000008"), new Guid("cccc0000-0000-0000-0000-000000000008") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000009"), new Guid("cccc0000-0000-0000-0000-000000000011") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000010"), new Guid("cccc0000-0000-0000-0000-000000000007") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000011"), new Guid("cccc0000-0000-0000-0000-000000000003") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000012"), new Guid("cccc0000-0000-0000-0000-000000000003") });

            migrationBuilder.DeleteData(
                table: "GenreMovie",
                keyColumns: new[] { "GenresId", "MoviesId" },
                keyValues: new object[] { new Guid("bbbb0000-0000-0000-0000-000000000012"), new Guid("cccc0000-0000-0000-0000-000000000012") });

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "MovieDetails",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666604"));

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666605"));

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666606"));

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666607"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000002"));
        }
    }
}
