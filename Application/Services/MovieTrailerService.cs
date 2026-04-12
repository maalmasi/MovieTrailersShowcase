using Application.Abstractions.Clients;
using Application.Abstractions.Services;
using Application.DTOs.Data;
using Application.DTOs.Response;
using Application.Helpers;
using Domain.Models.Response;

namespace Application.Services;

public class MovieTrailerService(ITmdbHttpClient tmdbHttpClient) : IMovieTrailerService
{
    public async Task<MovieListResponseDto> GetMovieTrailersAsync(string query, int page, string lang, bool displayAdult, string? region)
    {
        MovieInfoResponse movieInfoResponse = await tmdbHttpClient.GetMoviesAsync(query, page, lang, displayAdult, region);
        var moviesDto = new MovieListResponseDto
        {
            Movies = movieInfoResponse.Results.Select(movie => new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Adult = displayAdult,
                OriginalTitle = movie.OriginalTitle
            }).ToList(),
            Page = movieInfoResponse.Page,
            TotalPages = movieInfoResponse.TotalPages,
            TotalResults = movieInfoResponse.TotalResults
        };

        var movieTrailerTasks = new List<Task<MovieTrailerResponse>>();
        foreach (MovieDto movie in moviesDto.Movies)
        {
            Task<MovieTrailerResponse> task = tmdbHttpClient.GetMovieVideosById(movie.Id, lang);
            movieTrailerTasks.Add(task);
        }

        await Task.WhenAll(movieTrailerTasks);

        foreach (Task<MovieTrailerResponse> trailerTask in movieTrailerTasks)
        {
            MovieTrailerResponse trailerResponse = await trailerTask;
            MovieDto movie = moviesDto.Movies.First(m => m.Id == trailerResponse.Id);

            if (TrailerHelper.FindTrailer(trailerResponse.Results, movie))
            {
                continue;
            }
            else
            {
                // find trailer by movie title elsewhere
            }
        }

        return moviesDto;
    }

    public async Task<MovieCompleteInfoResponseDto> GetMovieCompleteInfoAsync(int id, string lang)
    {
        MovieCompleteInfoResponse movieInfo = await tmdbHttpClient.GetMovieDetailsById(id, lang);

        var movieDto = new MovieCompleteInfoResponseDto
        {
            Id = movieInfo.Id,
            Title = movieInfo.Title,
            OriginalTitle = movieInfo.OriginalTitle,
            Adult = movieInfo.Adult,
            BackdropPath = movieInfo.BackdropPath,
            BelongsToCollection = movieInfo.BelongsToCollection == null ? null : new CollectionDto
            {
                Id = movieInfo.BelongsToCollection.Id,
                Name = movieInfo.BelongsToCollection.Name,
                PosterPath = movieInfo.BelongsToCollection.PosterPath,
                BackdropPath = movieInfo.BelongsToCollection.BackdropPath
            },
            Budget = movieInfo.Budget,
            Genres = movieInfo.Genres?.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList(),
            Homepage = movieInfo.Homepage,
            ImdbId = movieInfo.ImdbId,
            OriginCountry = movieInfo.OriginCountry,
            OriginalLanguage = movieInfo.OriginalLanguage,
            Overview = movieInfo.Overview,
            Popularity = movieInfo.Popularity,
            PosterPath = movieInfo.PosterPath,
            ProductionCompanies = movieInfo.ProductionCompanies?.Select(pc => new ProductionCompanyDto
            {
                Id = pc.Id,
                LogoPath = pc.LogoPath,
                Name = pc.Name,
                OriginCountry = pc.OriginCountry
            }).ToList(),
            ProductionCountries = movieInfo.ProductionCountries?.Select(pc => new ProductionCountryDto
            {
                Iso3166_1 = pc.Iso3166_1,
                Name = pc.Name
            }).ToList(),
            ReleaseDate = movieInfo.ReleaseDate,
            Revenue = movieInfo.Revenue,
            Runtime = movieInfo.Runtime,
            SpokenLanguages = movieInfo.SpokenLanguages?.Select(sl => new SpokenLanguageDto
            {
                EnglishName = sl.EnglishName,
                Iso639_1 = sl.Iso639_1,
                Name = sl.Name
            }).ToList(),
            Status = movieInfo.Status,
            Tagline = movieInfo.Tagline,
            Video = movieInfo.Video,
            VoteAverage = movieInfo.VoteAverage,
            VoteCount = movieInfo.VoteCount
        };

        if (!TrailerHelper.FindTrailer(movieInfo.Videos.Results, movieDto))
        {
            // find trailer by movie title elsewhere
        }

        return movieDto;
    }
}
