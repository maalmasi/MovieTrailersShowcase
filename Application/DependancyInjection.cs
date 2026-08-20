thought
<channel|>```csharp
using Application.Abstractions.Services;
using Application.Services;
using Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
        services.AddTransient<IMovieTrailerService, MovieTrailerService>();

        return services;
    }
}

