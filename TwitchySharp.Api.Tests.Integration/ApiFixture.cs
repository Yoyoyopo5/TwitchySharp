using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using TwitchySharp.Api.Tests.Integration.Authorization;

namespace TwitchySharp.Api.Tests.Integration;
public class ApiFixture<TSecrets> : IDisposable
{
    public TwitchApi Api { get; }
    private readonly TwitchHttpClient _twitchHttpClient;
    private readonly HttpClient _httpClient;
    private readonly RateLimiter _rateLimiter;

    public TSecrets Secrets { get; }

    public ApiFixture(string secretsSection)
    {
        Secrets = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build().GetRequiredSection(secretsSection).Get<TSecrets>()!;

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
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        _rateLimiter.Dispose();
    }
}
