using Application.DTOs.Data;
using Domain.Models.Data;

namespace Application.Helpers;

public static class TrailerHelper
{
    private static readonly List<Func<MovieTrailer, bool>> movieTrailerPredicates =
    [
        v => v.Type == "Trailer" && v.Site == "YouTube" && v.Official,
        v => v.Type == "Trailer" && v.Official,
        v => v.Type == "Trailer" && (v.Site == "YouTube" || v.Site == "Vimeo")
    ];
    public static string BuildUrlFromSiteName(string siteName, string key)
    {
        return siteName switch
        {
            "YouTube" => $"https://www.youtube.com/watch?v={key}",
            "Vimeo" => $"https://vimeo.com/{key}",
            _ => ""
        };
    }

    public static bool FindTrailer(IEnumerable<MovieTrailer> trailers, MovieDto movieDto)
    {
        MovieTrailer? trailer = movieTrailerPredicates
                .Select(p => trailers.FirstOrDefault(p))
                .FirstOrDefault(v => v != null);

        if (trailer != null)
        {
            movieDto.TrailerUrl = TrailerHelper.BuildUrlFromSiteName(trailer.Site, trailer.Key);

            return true;
        }
        else
        {
            return false;
        }
    }
}
