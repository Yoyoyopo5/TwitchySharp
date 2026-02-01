using System.Net;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Api.Tests.Integration.Fixtures;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Integration.Tests;

public class Test_WarnChatUserRequest : IClassFixture<TwitchApiTestFixture>
{
    private readonly TwitchApiTestFixture _fixture;

    public Test_WarnChatUserRequest(TwitchApiTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResponseConfig.Reset();
    }

    [Fact]
    public async Task SendAsync_ValidRequest_ReturnsWarningData()
    {
        // Arrange
        var client = _fixture.CreateTwitchClient(_fixture.CreateDefaultAuthorizer());
        var request = new WarnChatUserRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            ModeratorId = new UserId("654321"),
            Warning = new WarnChatUserRequestData
            {
                Data = new ChatUserWarning
                {
                    UserId = new UserId("111222"),
                    Reason = "Please follow the chat rules"
                }
            }
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Content);
        Assert.NotNull(response.Content.Data);
        Assert.Single(response.Content.Data);
        Assert.Equal("123456", response.Content.Data[0].BroadcasterId.Value);
        Assert.Equal("654321", response.Content.Data[0].ModeratorId.Value);
        Assert.Equal("111222", response.Content.Data[0].UserId.Value);
        Assert.Equal("Please follow the chat rules", response.Content.Data[0].Reason);
    }

    [Fact]
    public async Task SendAsync_ValidRequest_ReturnsRateLimitHeaders()
    {
        // Arrange
        _fixture.ResponseConfig.RateLimitLimit = 100;
        _fixture.ResponseConfig.RateLimitRemaining = 50;

        var client = _fixture.CreateTwitchClient(_fixture.CreateDefaultAuthorizer());
        var request = new WarnChatUserRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            ModeratorId = new UserId("654321"),
            Warning = new WarnChatUserRequestData
            {
                Data = new ChatUserWarning
                {
                    UserId = new UserId("111222"),
                    Reason = "Test reason"
                }
            }
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.NotNull(response.RateLimitDetails);
        Assert.Equal(100, response.RateLimitDetails.Value.Limit);
        Assert.Equal(50, response.RateLimitDetails.Value.Remaining);
        Assert.NotNull(response.RateLimitDetails.Value.Reset);
    }

    [Fact]
    public async Task SendAsync_MissingClientIdHeader_ThrowsTwitchApiException()
    {
        // Arrange - No authorizer means no Client-Id header
        var client = _fixture.CreateTwitchClient(authorizer: null);
        var request = new WarnChatUserRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            ModeratorId = new UserId("654321"),
            Warning = new WarnChatUserRequestData
            {
                Data = new ChatUserWarning
                {
                    UserId = new UserId("111222"),
                    Reason = "Test reason"
                }
            }
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());
        Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
    }

    [Fact]
    public async Task SendAsync_ForcedServerError_ThrowsTwitchApiException()
    {
        // Arrange
        _fixture.ResponseConfig.ForceStatusCode = HttpStatusCode.InternalServerError;
        _fixture.ResponseConfig.ForceErrorMessage = "Internal server error";

        var client = _fixture.CreateTwitchClient(_fixture.CreateDefaultAuthorizer());
        var request = new WarnChatUserRequest
        {
            Host = "localhost",
            BroadcasterId = new UserId("123456"),
            ModeratorId = new UserId("654321"),
            Warning = new WarnChatUserRequestData
            {
                Data = new ChatUserWarning
                {
                    UserId = new UserId("111222"),
                    Reason = "Test reason"
                }
            }
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());
        Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
    }
}
