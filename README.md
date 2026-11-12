# FridayFilm

[![CI](https://github.com/YunusMaharramli/FridayFilm/actions/workflows/ci.yml/badge.svg)](https://github.com/YunusMaharramli/FridayFilm/actions/workflows/ci.yml)

**A movie catalog REST API built with ASP.NET Core, Clean Architecture and PostgreSQL.**

Users browse and search movies, actors, directors and genres. Administrators manage the catalog and upload images. Access is controlled with JWT authentication and permission-based roles.

Team project (2 developers) · Sep 2026 · planned in Jira, delivered through feature branches and pull requests

---

## Features

**Catalog**
- CRUD and search for movies, actors, directors, bios, genres, categories, languages and movie details
- Slugs for readable URLs, pagination, soft delete and audit fields (created/updated)

**Images**
- Image upload and deletion through Cloudinary
- Paginated image endpoints

**Authentication and authorization**
- ASP.NET Core Identity with JWT access tokens and a refresh token flow
- Email verification on sign-up
- Permission-based roles: each endpoint requires a permission, and roles are sets of permissions

**Quality**
- Request validation with FluentValidation
- A global exception handler that returns consistent error responses
- Read and write repositories kept separate

## Architecture

Clean Architecture. Each layer depends only on the layers inside it.

| Layer | Project | Responsibility |
| --- | --- | --- |
| Domain | `Core/FridayFilm.Domain` | Entities, enums, base entity |
| Application | `Core/FridayFilm.Application` | DTOs, validators, service and repository interfaces |
| Persistence | `Infrastructure/FridayFilm.Persistence` | EF Core `DbContext`, configurations, migrations, repositories |
| Infrastructure | `Infrastructure/FridayFilm.Infrastructure` | External services: Cloudinary, JWT, email |
| API | `Presentation/FridayFilm.WebApi` | Controllers, middleware, Swagger, a small web frontend |
| Tests | `Tests/FridayFilm.Tests` | Authentication and regression tests |

All projects live under `FridayFilm/`.

## Tech stack

.NET 10 · ASP.NET Core Web API · Entity Framework Core · PostgreSQL · ASP.NET Core Identity · JWT · FluentValidation · Cloudinary · Swagger · xUnit

## Run locally

**Requirements:** .NET 10 SDK, PostgreSQL, a Cloudinary account.

1. Create `FridayFilm/Presentation/FridayFilm.WebApi/appsettings.json` (it is not committed) with:
   - the PostgreSQL connection string
   - JWT settings (issuer, audience, signing key)
   - Cloudinary credentials
2. Apply the database migrations:
   ```powershell
   ./FridayFilm/migrate.ps1
   ```
   (or `FridayFilm/migrate.bat`)
3. Run the API:
   ```bash
   dotnet run --project FridayFilm/Presentation/FridayFilm.WebApi
   ```
4. Open Swagger at `/swagger`.

## Tests

```bash
dotnet test FridayFilm/FridayFilm.slnx
```

## Team

| Developer | Main areas |
| --- | --- |
| [Yunus Maharramli](https://github.com/YunusMaharramli) | Solution setup, read/write repositories, Cloudinary file service, global exception handler, JWT and refresh tokens, permission-based roles, movie CRUD, web frontend |
| [agilxalil](https://github.com/agilxalil) | DbContext and migrations, entity configurations, catalog endpoints, soft delete and auditing, FluentValidation, email verification, seed data |
