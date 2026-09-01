namespace TwitchySharp.Api.Tests.Unit.Client.RateLimiting;

public class Test_DefaultRateLimitCache
{
    [Fact]
    public async Task SetRateLimitDetails_ThenGetRateLimitDetails_ReturnsSetValue()
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
    public async Task SetRateLimitDetails_ThenGetRateLimitDetailsWithDifferentClientId_ReturnsNull()
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
}
