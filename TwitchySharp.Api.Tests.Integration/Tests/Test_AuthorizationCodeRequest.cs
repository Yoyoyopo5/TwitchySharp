using System.Net;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Tests.Integration.Fixtures;

namespace TwitchySharp.Api.Tests.Integration.Tests;

public class Test_AuthorizationCodeRequest : IClassFixture<TwitchApiTestFixture>
{
    private readonly TwitchApiTestFixture _fixture;

    public Test_AuthorizationCodeRequest(TwitchApiTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.ResponseConfig.Reset();
    }

    [Fact]
    public async Task SendAsync_ValidRequest_ReturnsTokens()
    {
        // Arrange
        var client = _fixture.CreateTwitchClientBuilder().Build();
        var request = new AuthorizationCodeRequest
        {
            Host = "localhost",
            ClientId = TwitchApiTestFixture.TestClientId,
            ClientSecret = TwitchApiTestFixture.TestClientSecret,
            Code = TwitchApiTestFixture.TEST_AUTHORIZATION_CODE,
            RedirectUri = TwitchApiTestFixture.TestRedirectUri
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Content);
        Assert.Equal(TwitchApiTestFixture.TEST_ACCESS_TOKEN, response.Content.AccessToken.Value);
        Assert.Equal(TwitchApiTestFixture.TEST_REFRESH_TOKEN, response.Content.RefreshToken.Value);
        Assert.Equal("bearer", response.Content.TokenType);
        Assert.True(response.Content.ExpiresIn.TotalSeconds > 0);
        Assert.NotNull(response.Content.Scope);
        Assert.Contains(Scope.ChannelModerate, response.Content.Scope);
    }

    [Fact]
    public async Task SendAsync_InvalidCode_ThrowsTwitchApiException()
    {
        // Arrange
        var client = _fixture.CreateTwitchClientBuilder().Build();
        var request = new AuthorizationCodeRequest
        {
            Host = "localhost",
            ClientId = TwitchApiTestFixture.TestClientId,
            ClientSecret = TwitchApiTestFixture.TestClientSecret,
            Code = "invalid_code",
            RedirectUri = TwitchApiTestFixture.TestRedirectUri
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }
}
