using Application.DTOs.Response;

namespace Application.Abstractions.Services;

public interface IMovieTrailerService
{
    Task<MovieListResponseDto> GetMovieTrailersAsync(string query, int page, string lang, bool displayAdult, string? region);
    Task<MovieCompleteInfoResponseDto> GetMovieCompleteInfoAsync(int id, string lang);
}
