using System.Diagnostics;
using System.Net;

namespace TwitchySharp.Api.Tests.Unit.Client.RateLimiting;

public class Test_WithRateLimiting
{
    private readonly static SendTwitchRequest StubSend = (request, _) => Task.FromResult(new TwitchResponse()
    {
        Request = request.Request,
        StatusCode = HttpStatusCode.OK,
        RateLimitDetails = null
    });

    private record TestTwitchRequest : TwitchRequest, IAuthorizedTwitchRequest
    {
        public override HttpMethod Method { get; } = HttpMethod.Get;
        public override Uri RequestUri { get; } = new("https://test.com");

        public TwitchRequestAuthorizationContext AuthorizationContext { get; init; } = new()
        {
            Identity = TwitchIdentity.Client.Default
        };
    }

    private record TestRateLimitCache : ITwitchRateLimitCache
    {
        public Func<ClientId, CancellationToken, ValueTask<TwitchRateLimitDetails?>> Get { get; init; } = (_, _) => throw new NotImplementedException();
        public Func<ClientId, TwitchRateLimitDetails, CancellationToken, ValueTask> Set { get; init; } = (_, _, _) => throw new NotImplementedException();
        public ValueTask<TwitchRateLimitDetails?> GetRateLimitDetails(ClientId clientId, CancellationToken ct) => Get(clientId, ct);
        public ValueTask SetRateLimitDetails(ClientId clientId, TwitchRateLimitDetails details, CancellationToken ct) => Set(clientId, details, ct);
    }

    [Fact]
    public async Task SendTwitchRequest_SomeRemaining_RequestCompletesImmediately()
    {
        const int CLOCK_SKEW_MS = 10;

        TestRateLimitCache mockCache = new()
        {
            Get = (_, _) => ValueTask.FromResult<TwitchRateLimitDetails?>(new TwitchRateLimitDetails()
            {
                Remaining = 1
            })
        };

        SendTwitchRequest mockSend = StubSend.WithRateLimiting(new() { Cache = mockCache, ClockSkew = TimeSpan.FromMilliseconds(CLOCK_SKEW_MS) });

        Stopwatch sw = Stopwatch.StartNew();
        await mockSend(new TestTwitchRequest(), TestContext.Current.CancellationToken);
        sw.Stop();

        Assert.True(sw.ElapsedMilliseconds < CLOCK_SKEW_MS);
    }

    [Fact]
    public async Task SendTwitchRequest_NoneRemaining_RequestWaitsForResetAndClockSkew()
    {
        const int CLOCK_SKEW_MS = 100;
        const int RESET_SECONDS = 1;

        TestRateLimitCache mockCache = new()
        {
            Get = (_, _) => ValueTask.FromResult<TwitchRateLimitDetails?>(new TwitchRateLimitDetails()
            {
                Remaining = 0,
                Reset = DateTimeOffset.UtcNow.AddSeconds(RESET_SECONDS)
            })
        };

        SendTwitchRequest mockSend = StubSend.WithRateLimiting(new() { Cache = mockCache, ClockSkew = TimeSpan.FromMilliseconds(CLOCK_SKEW_MS) });

        Stopwatch sw = Stopwatch.StartNew();
        await mockSend(new TestTwitchRequest(), TestContext.Current.CancellationToken);
        sw.Stop();

        Assert.True(sw.ElapsedMilliseconds > ((RESET_SECONDS * 1000) + CLOCK_SKEW_MS));
    }

    [Fact]
    public async Task SendTwitchRequest_NoneRemainingWithNullReset_RequestCompletesImmediately()
    {
        const int CLOCK_SKEW_MS = 100;

        TestRateLimitCache mockCache = new()
        {
            Get = (_, _) => ValueTask.FromResult<TwitchRateLimitDetails?>(new TwitchRateLimitDetails()
            {
                Remaining = 0,
                Reset = null
            })
        };

        SendTwitchRequest mockSend = StubSend.WithRateLimiting(new() { Cache = mockCache, ClockSkew = TimeSpan.FromMilliseconds(CLOCK_SKEW_MS) });

        Stopwatch sw = Stopwatch.StartNew();
        await mockSend(new TestTwitchRequest(), TestContext.Current.CancellationToken);
        sw.Stop();

        Assert.True(sw.ElapsedMilliseconds < CLOCK_SKEW_MS);
    }

    [Fact]
    public async Task SendTwitchRequest_AuthorizedRequest_CacheSeesClientId()
    {
        ClientId expectedClientId = new("12345");
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

        SendTwitchRequest mockSend = StubSend.WithRateLimiting(new() { Cache = mockCache });

        await mockSend(new TestTwitchRequest()
        {
            AuthorizationContext = new() { Identity = new TwitchIdentity.Client(expectedClientId) }
        }, TestContext.Current.CancellationToken);

        Assert.Equal(expectedClientId, cacheClientId);
    }

    [Fact]
    public async Task SendTwitchRequest_AuthorizationRequestContextWithClientId_CacheSeesClientId()
    {
        ClientId expectedClientId = new("12345");
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

        SendTwitchRequest mockSend = StubSend.WithRateLimiting(new() { Cache = mockCache });

        await mockSend(new TwitchAuthorizationRequestContext()
        {
            Request = new TestTwitchRequest(),
            AuthorizationHeaders = new() { ClientId = expectedClientId }
        }, TestContext.Current.CancellationToken);

        Assert.Equal(expectedClientId, cacheClientId);
    }

    [Fact]
    public async Task SendTwitchRequest_NoCachedLimit_RequestCompletesImmediately()
    {
        const int CLOCK_SKEW_MS = 20;

        TestRateLimitCache mockCache = new()
        {
            Get = (clientId, _) => ValueTask.FromResult<TwitchRateLimitDetails?>(null)
        };

        SendTwitchRequest mockSend = StubSend.WithRateLimiting(new() { Cache = mockCache });

        Stopwatch sw = Stopwatch.StartNew();
        await mockSend(new TestTwitchRequest(), TestContext.Current.CancellationToken);
        sw.Stop();

        Assert.True(sw.ElapsedMilliseconds < CLOCK_SKEW_MS);
    }

    [Fact]
    public async Task SendTwitchRequestWithStrictRateLimiting_InParallel_OneRemaining_OneRequestWaits()
    {
        const int CLOCK_SKEW_MS = 20;
        const int RESET_SECONDS = 1;

        int remaining = 1;

        TestRateLimitCache mockCache = new()
        {
            Get = (clientId, _) => ValueTask.FromResult<TwitchRateLimitDetails?>(new()
            {
                Remaining = remaining--,
                Reset = DateTimeOffset.UtcNow.AddSeconds(RESET_SECONDS)
            })
        };

        SendTwitchRequest mockSend = StubSend.WithStrictRateLimiting(new() { Cache = mockCache, ClockSkew = TimeSpan.FromMilliseconds(CLOCK_SKEW_MS) });

        ManualResetEventSlim gate = new(false);

        Task[] tasks = Enumerable.Range(0, 2).Select(_ => Task.Run(async () =>
        {
            gate.Wait(TestContext.Current.CancellationToken);
            await mockSend(new TestTwitchRequest(), TestContext.Current.CancellationToken);
        }, TestContext.Current.CancellationToken)).ToArray();

        Stopwatch sw = Stopwatch.StartNew();
        gate.Set();
        await Task.WhenAll(tasks);
        sw.Stop();

        Assert.True(sw.ElapsedMilliseconds > ((RESET_SECONDS * 1000) + CLOCK_SKEW_MS));
    }
}
