using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Tests.Integration.Authorization;
public class AuthorizationFixture : IDisposable
{
    public TwitchApi Api { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
    public string RedirectUrl => "http://localhost:5000";
    private readonly TwitchHttpClient _twitchHttpClient;
    private readonly HttpClient _httpClient;
    private readonly RateLimiter _rateLimiter;

    public AuthorizationFixture()
    {
        _httpClient = new HttpClient();
        _rateLimiter = new TokenBucketRateLimiter(new TokenBucketRateLimiterOptions()
        {
            QueueLimit = 100,
            TokenLimit = 800,
            TokensPerPeriod = 800,
            ReplenishmentPeriod = TimeSpan.FromHours(6) // I believe this is standard.
        });
        _twitchHttpClient = new TwitchHttpClient(_httpClient, _rateLimiter);
        Api = new TwitchApi(_twitchHttpClient);

        ClientId = System.Environment.GetEnvironmentVariable("TWITCHYSHARP_TEST_CLIENT_ID", EnvironmentVariableTarget.User)!;
        ClientSecret = System.Environment.GetEnvironmentVariable("TWITCHYSHARP_TEST_CLIENT_SECRET", EnvironmentVariableTarget.User)!;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        _rateLimiter.Dispose();
    }
}

[CollectionDefinition("authorization")]
public class AuthorizationCollection : ICollectionFixture<AuthorizationFixture> { }
