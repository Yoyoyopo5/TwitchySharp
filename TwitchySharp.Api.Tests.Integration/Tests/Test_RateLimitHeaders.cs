using System;
using System.Net;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Api.Tests.Integration.Fixtures;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Integration.Tests;

/// <summary>
/// Tests for rate limit header parsing via ToTwitchRateLimitDetails extension.
/// </summary>
public class Test_RateLimitHeaders : IClassFixture<TwitchApiTestFixture>
{
    private readonly TwitchApiTestFixture _fixture;

    public Test_RateLimitHeaders(TwitchApiTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResponseConfig.Reset();
    }

    [Fact]
    public async Task RateLimitHeaders_ParsedCorrectly_Limit()
    {
        // Arrange
        _fixture.ResponseConfig.RateLimitLimit = 800;

        var client = _fixture.CreateTwitchClient(_fixture.CreateDefaultAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.NotNull(response.RateLimitDetails);
        Assert.Equal(800, response.RateLimitDetails.Value.Limit);
    }

    [Fact]
    public async Task RateLimitHeaders_ParsedCorrectly_Remaining()
    {
        // Arrange
        _fixture.ResponseConfig.RateLimitRemaining = 123;

        var client = _fixture.CreateTwitchClient(_fixture.CreateDefaultAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.NotNull(response.RateLimitDetails);
        Assert.Equal(123, response.RateLimitDetails.Value.Remaining);
    }

    [Fact]
    public async Task RateLimitHeaders_ParsedCorrectly_Reset()
    {
        // Arrange
        var expectedReset = DateTimeOffset.UtcNow.AddMinutes(5);
        _fixture.ResponseConfig.RateLimitReset = expectedReset;

        var client = _fixture.CreateTwitchClient(_fixture.CreateDefaultAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.NotNull(response.RateLimitDetails);
        Assert.NotNull(response.RateLimitDetails.Value.Reset);

        // Compare Unix timestamps (rounded to seconds)
        var expectedUnix = expectedReset.ToUnixTimeSeconds();
        var actualUnix = response.RateLimitDetails.Value.Reset!.Value.ToUnixTimeSeconds();
        Assert.Equal(expectedUnix, actualUnix);
    }

    [Fact]
    public async Task RateLimitHeaders_AllFieldsPresent()
    {
        // Arrange
        var resetTime = DateTimeOffset.UtcNow.AddMinutes(10);
        _fixture.ResponseConfig.RateLimitLimit = 500;
        _fixture.ResponseConfig.RateLimitRemaining = 250;
        _fixture.ResponseConfig.RateLimitReset = resetTime;

        var client = _fixture.CreateTwitchClient(_fixture.CreateDefaultAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.NotNull(response.RateLimitDetails);
        var details = response.RateLimitDetails.Value;
        Assert.Equal(500, details.Limit);
        Assert.Equal(250, details.Remaining);
        Assert.NotNull(details.Reset);
    }

    [Fact]
    public async Task RateLimitHeaders_ZeroRemaining_ParsedCorrectly()
    {
        // Arrange - Simulate rate limit exhausted
        _fixture.ResponseConfig.RateLimitLimit = 800;
        _fixture.ResponseConfig.RateLimitRemaining = 0;

        var client = _fixture.CreateTwitchClient(_fixture.CreateDefaultAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.NotNull(response.RateLimitDetails);
        Assert.Equal(0, response.RateLimitDetails.Value.Remaining);
    }
}
