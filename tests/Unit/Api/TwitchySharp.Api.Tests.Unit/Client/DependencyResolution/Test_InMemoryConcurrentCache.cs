using TwitchySharp.Tests.Unit.Toolkit;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;

public class Test_InMemoryConcurrentCache
{
    [Fact]
    public async Task Get_AfterSet_ReturnsSetValue()
    {
        ClientId stubClientId = new("12345");
        TwitchRateLimitDetails stubDetails = new()
        {
            Limit = 100,
            Remaining = 99,
            Reset = new DateTimeOffset(2026, 6, 26, 18, 8, 0, TimeSpan.Zero)
        };

        InMemoryConcurrentCache<ClientId, TwitchRateLimitDetails?> mockCache = new();
        await mockCache.Set(stubClientId, stubDetails, TestContext.Current.CancellationToken);

        TwitchRateLimitDetails? result = await mockCache.GetOrDefault(stubClientId, TestContext.Current.CancellationToken);

        Assert.Equal(stubDetails, result);
    }

    [Fact]
    public async Task Get_WithoutSet_ReturnsNull()
    {
        ClientId stubClientId = new("12345");
        InMemoryConcurrentCache<ClientId, TwitchRateLimitDetails?> mockCache = new();

        TwitchRateLimitDetails? details = await mockCache.GetOrDefault(stubClientId, TestContext.Current.CancellationToken);

        Assert.Null(details);
    }

    [Fact]
    public async Task Get_AfterSetNull_ReturnsNull()
    {
        ClientId stubClientId = new("12345");
        TwitchRateLimitDetails fakeDetails = default;

        InMemoryConcurrentCache<ClientId, TwitchRateLimitDetails?> mockCache = new();
        await mockCache.Set(stubClientId, fakeDetails, TestContext.Current.CancellationToken);
        await mockCache.Set(stubClientId, null, TestContext.Current.CancellationToken);

        TwitchRateLimitDetails? actual = await mockCache.GetOrDefault(stubClientId, TestContext.Current.CancellationToken);

        Assert.Null(actual);
    }

    [Fact]
    public async Task GetOrDefault_AfterSetWithDifferentKey_ReturnsNull()
    {
        ClientId stubClientId = new("12345");
        TwitchRateLimitDetails stubDetails = new()
        {
            Limit = 100,
            Remaining = 99,
            Reset = new DateTimeOffset(2026, 6, 26, 18, 8, 0, TimeSpan.Zero)
        };

        InMemoryConcurrentCache<ClientId, TwitchRateLimitDetails?> mockCache = new();
        await mockCache.Set(stubClientId, stubDetails, TestContext.Current.CancellationToken);

        TwitchRateLimitDetails? result = await mockCache.GetOrDefault(new("29378"), TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAndSetConcurrently_EachWorkerSets()
    {
        const int WORKER_COUNT = 512;
        CancellationToken ct = TestContext.Current.CancellationToken;

        InMemoryConcurrentCache<int, TwitchRateLimitDetails?> cache = new();

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            await cache.Set(i, new TwitchRateLimitDetails(), ct);
        }, ct);

        await Task.WhenAll(Enumerable.Range(0, WORKER_COUNT).Select(async i =>
        {
            Assert.NotNull(await cache.GetOrDefault(i, ct));
            return i;
        }));
    }
}
