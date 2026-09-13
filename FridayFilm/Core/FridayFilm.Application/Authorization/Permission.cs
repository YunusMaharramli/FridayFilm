namespace FridayFilm.Application.Authorization;
public static class Permissions
{
    public static class Movies
    {
        public const string Read = "movies.read";
        public const string Create = "movies.create";
        public const string Update = "movies.update";
        public const string Delete = "movies.delete";
    }

    public static class Roles
    {
        public const string Read = "roles.read";
        public const string Create = "roles.create";
        public const string Update = "roles.update";
    }

    public static class Actors
    {
        public const string Read = "actors.read";
        public const string Create = "actors.create";
        public const string Update = "actors.update";
        public const string Delete = "actors.delete";
    }

    public static class Directors
    {
        public const string Read = "directors.read";
        public const string Create = "directors.create";
        public const string Update = "directors.update";
        public const string Delete = "directors.delete";
    }

    public static class Categories
    {
        public const string Read = "categories.read";
        public const string Create = "categories.create";
        public const string Update = "categories.update";
        public const string Delete = "categories.delete";
    }

    public static class Genres
    {
        public const string Read = "genres.read";
        public const string Create = "genres.create";
        public const string Update = "genres.update";
        public const string Delete = "genres.delete";
    }

    public static class Bios
    {
        public const string Read = "bios.read";
        public const string Create = "bios.create";
        public const string Update = "bios.update";
        public const string Delete = "bios.delete";
    }

    public static class MovieDetails
    {
        public const string Read = "movie-details.read";
        public const string Create = "movie-details.create";
        public const string Update = "movie-details.update";
        public const string Delete = "movie-details.delete";
    }

    public static class Images
    {
        public const string Read = "images.read";
    }

    public static readonly IReadOnlyCollection<string> All =
    [
        Movies.Read,
        Movies.Create,
        Movies.Update,
        Movies.Delete,
        Roles.Read,
        Roles.Create,
        Roles.Update,

        Actors.Read,
        Actors.Create,
        Actors.Update,
        Actors.Delete,

        Directors.Read,
        Directors.Create,
        Directors.Update,
        Directors.Delete,

        Categories.Read,
        Categories.Create,
        Categories.Update,
        Categories.Delete,

        Genres.Read,
        Genres.Create,
        Genres.Update,
        Genres.Delete,

        Bios.Read,
        Bios.Create,
        Bios.Update,
        Bios.Delete,

        MovieDetails.Read,
        MovieDetails.Create,
        MovieDetails.Update,
        MovieDetails.Delete,

        Images.Read
    ];

    public static bool IsValid(string permission)
    {
        return All.Contains(
            permission,
            StringComparer.OrdinalIgnoreCase);
    }
}
