using System.Diagnostics;

namespace TwitchySharp.Api.Tests.Unit.Client.RateLimiting;

public static class TestHttpResponseMessageExtensions
{
    public static HttpResponseMessage AddRateLimitDetails(
        this HttpResponseMessage response,
        TwitchRateLimitDetails details
        )
    {
        response.Headers.Add("Ratelimit-Limit", details.Limit.ToString());
        response.Headers.Add("Ratelimit-Remaining", details.Remaining.ToString());
        response.Headers.Add("Ratelimit-Reset", details.Reset.ToString());
        return response;
    }
}

public class Test_WithRateLimiting
{
    private record TestTwitchRequest : TwitchRequest<object>,
        IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Client>>
    {
        public override HttpMethod Method { get; } = HttpMethod.Get;
        public override Uri RequestUri { get; } = new("https://test.com");

        public TwitchRequestAuthenticationContext<TwitchIdentity.Client> AuthenticationContext { get; init; } = new()
        {
            Identity = new(new("default"))
        };

        public override Func<Stream, CancellationToken, ValueTask<object>>? ConvertResponseContent { get; init; } = (_, _) => ValueTask.FromResult(new object());
    }

    private static TwitchClient CreateStubClient(TwitchRateLimitDetails? rateLimitDetails = null)
        => new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .SetResolver(_ => new HttpResponseMessage().AddRateLimitDetails(rateLimitDetails ?? DefaultRateLimitDetails));

    private readonly static TwitchRateLimitDetails DefaultRateLimitDetails = new()
    {
        Limit = 800,
        Remaining = 799,
        Reset = DateTimeOffset.MaxValue
    };

    private record TestRateLimitCache : ITwitchRateLimitCache
    {
        public Func<ClientId, CancellationToken, ValueTask<TwitchRateLimitDetails?>> Get { get; init; } = (_, _) => ValueTask.FromResult<TwitchRateLimitDetails?>(null);
        public Func<ClientId, TwitchRateLimitDetails?, CancellationToken, ValueTask> Set { get; init; } = (_, _, _) => ValueTask.CompletedTask;
        public ValueTask<TwitchRateLimitDetails?> GetRateLimitDetails(ClientId clientId, CancellationToken ct) => Get(clientId, ct);
        public ValueTask SetRateLimitDetails(ClientId clientId, TwitchRateLimitDetails? details, CancellationToken ct) => Set(clientId, details, ct);
    }

    [Fact]
    public async Task SendTwitchRequest_NoneRemaining_RequestWaitsForReset()
    {
        const int RESET_SECONDS = 1;
        DateTimeOffset now = DateTimeOffset.MinValue;

        TestRateLimitCache mockCache = new()
        {
            Get = (_, _) => ValueTask.FromResult<TwitchRateLimitDetails?>(new TwitchRateLimitDetails()
            {
                Remaining = 0,
                Reset = now.AddSeconds(RESET_SECONDS)
            })
        };

        TwitchClient client = CreateStubClient()
            .SetFixed<TwitchClient, ClientId?>(new("default"))
            .WithRateLimiting(options => options with
            {
                Cache = mockCache,
                GetNow = () => now
            });

        Stopwatch sw = Stopwatch.StartNew();
        await client.SendAsync(new TestTwitchRequest(), TestContext.Current.CancellationToken);
        sw.Stop();

        Assert.True(sw.ElapsedMilliseconds > (RESET_SECONDS * 1000));
    }

    [Fact]
    public async Task SendTwitchRequest_WithConfiguredClientId_CacheSeesClientId()
    {
        ClientId? expectedClientId = new("12345");
        ClientId? cacheClientId = null;

        TestRateLimitCache mockCache = new()
        {
            Get = (clientId, _) =>
            {
                cacheClientId = clientId;
                return ValueTask.FromResult<TwitchRateLimitDetails?>(new TwitchRateLimitDetails()
                {
                    Remaining = 1
                });
            }
        };

        TwitchClient client = CreateStubClient()
            .SetFixed(expectedClientId)
            .WithRateLimiting(options => options with
            {
                Cache = mockCache
            });

        await client.SendAsync(new TestTwitchRequest(), TestContext.Current.CancellationToken);

        Assert.Equal(expectedClientId, cacheClientId);
    }

    [Fact]
    public async Task SendTwitchRequestWithRateLimiting_InParallel_OneRemaining_OneRequestWaits()
    {
        DateTimeOffset now = DateTimeOffset.MinValue;
        const int RESET_SECONDS = 1;

        int remaining = 1;

        TestRateLimitCache mockCache = new()
        {
            Get = (clientId, _) => ValueTask.FromResult<TwitchRateLimitDetails?>(new()
            {
                Remaining = remaining--,
                Reset = now.AddSeconds(RESET_SECONDS)
            })
        };

        TwitchClient client = CreateStubClient()
            .SetFixed<TwitchClient, ClientId?>(new("default"))
            .WithRateLimiting(options => options with
            {
                Cache = mockCache,
                GetNow = () => now
            })
            .SerializeRequestsByClientId();

        ManualResetEventSlim gate = new(false);

        Task[] tasks = Enumerable.Range(0, 2).Select(_ => Task.Run(async () =>
        {
            gate.Wait(TestContext.Current.CancellationToken);
            await client.SendAsync(new TestTwitchRequest(), TestContext.Current.CancellationToken);
        }, TestContext.Current.CancellationToken)).ToArray();

        Stopwatch sw = Stopwatch.StartNew();
        gate.Set();
        await Task.WhenAll(tasks);
        sw.Stop();

        Assert.True(sw.ElapsedMilliseconds > (RESET_SECONDS * 1000));
    }
}
