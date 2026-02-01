using System.Net;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.AuthorizationResolution.AccessTokenResolvers;
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

    private IAuthorizeTwitchRequest CreateAuthorizer()
    {
        return new DefaultRequestAuthorizer(
            new SingleClientIdentityResolver(new ClientIdentity(new ClientId(TwitchApiTestFixture.TestClientId))),
            new SingleAccessTokenResolver(new UserAccessToken(TwitchApiTestFixture.TestAccessToken))
        );
    }

    [Fact]
    public async Task SendAsync_ValidRequest_Returns204NoContent()
    {
        // Arrange
        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.NotNull(response.Content); // EmptyResponseConverter returns empty object
    }

    [Fact]
    public async Task SendAsync_ValidRequest_ReturnsRateLimitHeaders()
    {
        // Arrange
        _fixture.ResponseConfig.RateLimitLimit = 800;
        _fixture.ResponseConfig.RateLimitRemaining = 799;

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
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
        Assert.Equal(799, response.RateLimitDetails.Value.Remaining);
    }

    [Fact]
    public async Task SendAsync_MissingAuthorizationHeader_ThrowsTwitchApiException()
    {
        // Arrange - No authorizer means no Authorization header
        var client = _fixture.CreateTwitchClient(authorizer: null);
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
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

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());
        Assert.Equal(HttpStatusCode.TooManyRequests, exception.StatusCode);
    }
}
