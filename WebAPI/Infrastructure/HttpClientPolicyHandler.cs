using Polly;
using Polly.Extensions.Http;

namespace WebAPI.Infrastructure;

internal static class HttpClientPolicyHandler
{
    internal static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
