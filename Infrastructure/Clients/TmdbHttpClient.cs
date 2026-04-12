using Application.Abstractions.Clients;
using Application.Exceptions;
using Domain.Models.Response;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Infrastructure.Clients;

public class TmdbHttpClient : ITmdbHttpClient
{
    private readonly string clientName;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IHttpClientFactory httpClientFactory;

    public TmdbHttpClient(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration
        )
    {
        clientName = configuration.GetValue<string>("HttpClients:TMDB:ClientName", "TMDBClient");
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<MovieInfoResponse> GetMoviesAsync(string query, int page, string lang, bool displayAdult, string? region)
    {
        HttpClient httpClient = httpClientFactory.CreateClient(clientName);

        string queryString = $"query={query}&page={page}&lang={lang}&displayAdult={displayAdult}";
        if (!string.IsNullOrWhiteSpace(region))
        {
            queryString = $"{queryString}&region={region}";
        }

        HttpResponseMessage response = await httpClient.GetAsync($"search/movie?{queryString}");
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MovieInfoResponse>(responseString, _serializerOptions)!;
    }

    public async Task<MovieTrailerResponse> GetMovieVideosById(int id, string lang)
    {
        HttpClient httpClient = httpClientFactory.CreateClient(clientName);

        string queryString = !string.IsNullOrWhiteSpace(lang) ? $"?language={lang}" : string.Empty;

        HttpResponseMessage response = await httpClient.GetAsync($"movie/{id}/videos{queryString}");

        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MovieTrailerResponse>(responseString, _serializerOptions)!;
    }

    public async Task<MovieCompleteInfoResponse> GetMovieDetailsById(int id, string lang)
    {
        HttpClient httpClient = httpClientFactory.CreateClient(clientName);

        string queryString = !string.IsNullOrWhiteSpace(lang) ? $"?language={lang}" : string.Empty;
        queryString = $"{queryString}&append_to_response=videos";
        HttpResponseMessage response = await httpClient.GetAsync($"movie/{id}{queryString}");

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MovieCompleteInfoResponse>(responseString, _serializerOptions)!;
        }
        else
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException($"Movie with id {id} not found.");
            }
            else
            {
                throw new Exception($"Failed to retrieve movie details. Status code: {response.StatusCode}");
            }
        }
    }
}
