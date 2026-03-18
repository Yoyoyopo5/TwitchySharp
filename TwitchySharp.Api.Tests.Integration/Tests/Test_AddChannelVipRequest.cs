using System.Net;
using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Api.Tests.Integration.Fixtures;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Integration.Tests;

public class Test_AddChannelVipRequest : IClassFixture<TwitchApiTestFixture>
{
    private readonly TwitchApiTestFixture _fixture;

    public Test_AddChannelVipRequest(TwitchApiTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResponseConfig.Reset();
    }

    [Fact]
    public async Task SendAsync_ValidRequest_Returns204NoContent()
    {
        // Arrange
        ITwitchClient client = _fixture.CreateTwitchClientBuilder().Build();
        AddChannelVipRequest request = new()
        {
            Host = "localhost",
            BroadcasterId = TwitchApiTestFixture.TestUserId,
            UserId = new UserId("654321")
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.NotNull(response.Content);
    }

    [Fact]
    public async Task SendAsync_ValidRequest_ReturnsRateLimitHeaders()
    {
        // Arrange
        _fixture.ResponseConfig.RateLimitLimit = 800;
        _fixture.ResponseConfig.RateLimitRemaining = 799;

        var client = _fixture.CreateTwitchClientBuilder().Build();
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = TwitchApiTestFixture.TestUserId,
            UserId = new UserId("654321")
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.NotNull(response.RateLimitDetails);
        Assert.Equal(800, response.RateLimitDetails.Value.Limit);
        Assert.Equal(799, response.RateLimitDetails.Value.Remaining);
    }

    [Fact]
    public async Task SendAsync_MissingAuthorizationHeader_ThrowsTwitchApiException()
    {
        var client = _fixture.CreateTwitchClientBuilder().Build();
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"), // UserId we don't have cached token for.
            UserId = new UserId("654321")
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());
        Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
    }

    [Fact]
    public async Task SendAsync_Forced429RateLimit_ThrowsTwitchApiException()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.TooManyRequests;
        _fixture.ResponseConfig.ForceErrorMessage = "Rate limit exceeded";

        var client = _fixture.CreateTwitchClientBuilder().Build();
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = TwitchApiTestFixture.TestUserId,
            UserId = new UserId("654321")
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());
        Assert.Equal(HttpStatusCode.TooManyRequests, exception.StatusCode);
    }
}
