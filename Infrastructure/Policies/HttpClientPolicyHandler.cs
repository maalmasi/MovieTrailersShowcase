using Polly;
using Polly.Extensions.Http;

namespace Infrastructure.Policies;

internal static class HttpClientPolicyHandler
{
    internal static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int maxRetryCount)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(maxRetryCount, retryAttempt)));
    }
}
