# FridayFilm

Movie catalog REST API built by a team of two with Clean Architecture, ASP.NET Core and PostgreSQL. Work was planned in Jira and delivered through feature branches and pull requests.

## Features

- CRUD and search for movies, actors, directors, bios, genres, categories, languages and movie details
- Image upload and deletion through Cloudinary, with paginated image endpoints
- Authentication and roles with ASP.NET Core Identity and JWT
- FluentValidation request validation and a global exception handler
- Soft delete, auditing fields, slugs and pagination
- Read/write repository separation

## Tech stack

.NET 10 · ASP.NET Core Web API · Entity Framework Core · PostgreSQL · ASP.NET Core Identity · JWT · FluentValidation · Cloudinary · Swagger · xUnit

## Project structure

All projects live under `FridayFilm/`.

| Layer | Project |
| --- | --- |
| Domain | `Core/FridayFilm.Domain` |
| Application | `Core/FridayFilm.Application` |
| Persistence | `Infrastructure/FridayFilm.Persistence` |
| Infrastructure | `Infrastructure/FridayFilm.Infrastructure` |
| API | `Presentation/FridayFilm.WebApi` |
| Tests | `Tests/FridayFilm.Tests` |

## Running locally

1. Set the PostgreSQL connection string, JWT settings and Cloudinary credentials in `FridayFilm/Presentation/FridayFilm.WebApi/appsettings.json` (not committed).
2. Apply migrations with `migrate.ps1` (or `migrate.bat`).
3. Run the API: `dotnet run --project FridayFilm/Presentation/FridayFilm.WebApi`
4. Open Swagger at `/swagger`.
