using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.AuthorizationResolution.AccessTokenResolvers;
using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Api.Tests.Integration.Fixtures;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Integration.Tests;

/// <summary>
/// Tests for TwitchApiException population and error handling.
/// </summary>
public class Test_TwitchApiException : IClassFixture<TwitchApiTestFixture>
{
    private readonly TwitchApiTestFixture _fixture;

    public Test_TwitchApiException(TwitchApiTestFixture fixture)
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
    public async Task Exception_400BadRequest_ContainsCorrectStatusCode()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.BadRequest;

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Fact]
    public async Task Exception_401Unauthorized_ContainsCorrectStatusCode()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.Unauthorized;

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
    }

    [Fact]
    public async Task Exception_404NotFound_ContainsCorrectStatusCode()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.NotFound;

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task Exception_429TooManyRequests_ContainsCorrectStatusCode()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.TooManyRequests;

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());

        // Assert
        Assert.Equal(HttpStatusCode.TooManyRequests, exception.StatusCode);
    }

    [Fact]
    public async Task Exception_500InternalServerError_ContainsCorrectStatusCode()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.InternalServerError;

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Exception_ContainsOriginalRequest()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.BadRequest;

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());

        // Assert
        Assert.NotNull(exception.Request);
        Assert.Same(request, exception.Request);
    }

    [Fact]
    public async Task Exception_ContainsResponseHeaders()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.BadRequest;

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());

        // Assert
        Assert.NotNull(exception.Headers);
    }

    [Fact]
    public async Task Exception_ContainsResponseBody()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.BadRequest;
        _fixture.ResponseConfig.ForceErrorMessage = "Test error message";

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());

        // Assert
        Assert.NotNull(exception.Content);
        Assert.NotEmpty(exception.Content);

        // Verify the error message is in the content
        var contentString = Encoding.UTF8.GetString(exception.Content);
        Assert.Contains("Test error message", contentString);
    }

    [Fact]
    public async Task Exception_ContainsContentHeaders()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.BadRequest;

        var client = _fixture.CreateTwitchClient(CreateAuthorizer());
        var request = new AddChannelVipRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            UserId = new UserId("654321")
        };

        // Act
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());

        // Assert
        Assert.NotNull(exception.ContentHeaders);
        Assert.True(exception.ContentHeaders.ContainsKey("Content-Type"));
    }
}
