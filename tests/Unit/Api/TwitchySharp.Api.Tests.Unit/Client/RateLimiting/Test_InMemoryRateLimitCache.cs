using TwitchySharp.Tests.Unit.Toolkit;

namespace TwitchySharp.Api.Tests.Unit.Client.RateLimiting;

public class Test_InMemoryRateLimitCache
{
    [Fact]
    public async Task GetRateLimitDetails_AfterSetDetails_ReturnsSetDetails()
    {
        ClientId stubClientId = new("12345");
        TwitchRateLimitDetails stubDetails = new()
        {
            Limit = 100,
            Remaining = 99,
            Reset = new DateTimeOffset(2026, 6, 26, 18, 8, 0, TimeSpan.Zero)
        };

        InMemoryRateLimitCache mockCache = new();
        await mockCache.SetRateLimitDetails(stubClientId, stubDetails, TestContext.Current.CancellationToken);

        TwitchRateLimitDetails? result = await mockCache.GetRateLimitDetails(stubClientId, TestContext.Current.CancellationToken);

        Assert.Equal(stubDetails, result);
    }

    [Fact]
    public async Task GetRateLimitDetails_WithoutSetDetails_ReturnsNull()
    {
        ClientId stubClientId = new("12345");
        InMemoryRateLimitCache mockCache = new();

        TwitchRateLimitDetails? details = await mockCache.GetRateLimitDetails(stubClientId, TestContext.Current.CancellationToken);

        Assert.Null(details);
    }

    [Fact]
    public async Task GetRateLimitDetails_AfterSetNullDetails_ReturnsNull()
    {
        ClientId stubClientId = new("12345");
        TwitchRateLimitDetails fakeDetails = default;

        InMemoryRateLimitCache mockCache = new();
        await mockCache.SetRateLimitDetails(stubClientId, fakeDetails, TestContext.Current.CancellationToken);
        await mockCache.SetRateLimitDetails(stubClientId, null, TestContext.Current.CancellationToken);

        TwitchRateLimitDetails? actual = await mockCache.GetRateLimitDetails(stubClientId, TestContext.Current.CancellationToken);

        Assert.Null(actual);
    }

    [Fact]
    public async Task GetRateLimitDetails_AfterSetDetailsWithDifferentClientId_ReturnsSetDetails()
    {
        ClientId stubClientId = new("12345");
        TwitchRateLimitDetails stubDetails = new()
        {
            Limit = 100,
            Remaining = 99,
            Reset = new DateTimeOffset(2026, 6, 26, 18, 8, 0, TimeSpan.Zero)
        };

        InMemoryRateLimitCache mockCache = new();
        await mockCache.SetRateLimitDetails(stubClientId, stubDetails, TestContext.Current.CancellationToken);

        TwitchRateLimitDetails? result = await mockCache.GetRateLimitDetails(new("29378"), TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAndSetRateLimitDetailsConcurrently_EachWorkerSetsDetails()
    {
        const int WORKER_COUNT = 512;
        CancellationToken ct = TestContext.Current.CancellationToken;

        InMemoryRateLimitCache cache = new();

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            ClientId clientId = new(i.ToString());
            await cache.SetRateLimitDetails(clientId, new TwitchRateLimitDetails(), ct);
            Assert.NotNull(await cache.GetRateLimitDetails(clientId, ct));
        }, ct);
    }
}
