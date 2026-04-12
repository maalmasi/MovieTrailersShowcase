using Application.Abstractions.Clients;
using Infrastructure.Clients;
using Infrastructure.Policies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTMDBClient(configuration);
        services.AddSingleton<ITmdbHttpClient, TmdbHttpClient>();

        return services;
    }

    private static IServiceCollection AddTMDBClient(this IServiceCollection services, IConfiguration configuration)
    {
        string tmdbClientName = configuration.GetValue<string>("HttpClients:TMDB:ClientName", "TMDBClient");
        int tmdbClientTimeout = configuration.GetValue<int>("HttpClients:TMDB:TimeoutSeconds", 10);
        int tmdbClientMaxRetryCount = configuration.GetValue<int>("HttpClients:TMDB:MaxRetryCount", 3);

        services.AddHttpClient(tmdbClientName, client =>
        {
            client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            client.Timeout = TimeSpan.FromSeconds(tmdbClientTimeout);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", configuration.GetValue<string>("HttpClients:TMDB:ApiKey"));
        })
            .AddPolicyHandler(HttpClientPolicyHandler.GetRetryPolicy(tmdbClientMaxRetryCount));

        return services;
    }
}