using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Tests.Integration.Authorization;
public class AuthorizationFixture : IDisposable
{
    public TwitchApi Api { get; }
    public AuthorizationSecrets Secrets { get; }
    private readonly TwitchHttpClient _twitchHttpClient;
    private readonly HttpClient _httpClient;
    private readonly RateLimiter _rateLimiter;


    public AuthorizationFixture()
    {
        Secrets = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build().GetRequiredSection("Authorization").Get<AuthorizationSecrets>()!;
        
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

public record AuthorizationSecrets
{
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string RedirectUri { get; init; }
    public required string UserAccessToken { get; init; }
    public required string UserRefreshToken { get; init; }
    public required string UserAuthorizationCode { get; init; }
    public required string DeviceAuthorizationCode { get; init; }
}
