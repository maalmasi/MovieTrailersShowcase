using Domain.Models.Response;

namespace Application.Abstractions.Clients;

public interface ITmdbHttpClient
{
    Task<MovieInfoResponse> GetMoviesAsync(string query, int page, string lang, bool displayAdult, string? region);
    Task<MovieTrailerResponse> GetMovieVideosById(int id, string lang);
    Task<MovieCompleteInfoResponse> GetMovieDetailsById(int id, string lang);
}
