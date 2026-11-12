using System.Text.RegularExpressions;

namespace FridayFilm.Application.Extensions
{
    public static class StringExtensions
    {
        public static string ToSlug(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            var slug = text.ToLowerInvariant();
            slug = slug.Replace("ə", "e").Replace("ö", "o").Replace("ğ", "g")
                       .Replace("ü", "u").Replace("ş", "s").Replace("ç", "c")
                       .Replace("ı", "i");
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-").Trim('-');

            return slug;
        }
    }
}