using System;
using System.Net;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Tests.Integration.Fixtures;
using TwitchySharp.Shared.Models;

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
        var client = _fixture.CreateTwitchClient(authorizer: null);
        var request = new AuthorizationCodeRequest
        {
            Host = "localhost", // Use test server
            ClientId = new ClientId(TwitchApiTestFixture.TestClientId),
            ClientSecret = new ClientSecret(TwitchApiTestFixture.TestClientSecret),
            Code = TwitchApiTestFixture.TestAuthorizationCode,
            RedirectUri = new Uri(TwitchApiTestFixture.TestRedirectUri)
        };

        // Act
        var response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Content);
        Assert.Equal(TwitchApiTestFixture.TestAccessToken, response.Content.AccessToken.Value);
        Assert.Equal(TwitchApiTestFixture.TestRefreshToken, response.Content.RefreshToken.Value);
        Assert.Equal("bearer", response.Content.TokenType);
        Assert.True(response.Content.ExpiresIn.TotalSeconds > 0);
        Assert.NotNull(response.Content.Scope);
        Assert.Contains(Scope.ChannelModerate, response.Content.Scope);
    }

    [Fact]
    public async Task SendAsync_InvalidCode_ThrowsTwitchApiException()
    {
        // Arrange
        var client = _fixture.CreateTwitchClient(authorizer: null);
        var request = new AuthorizationCodeRequest
        {
            Host = "localhost",
            ClientId = new ClientId(TwitchApiTestFixture.TestClientId),
            ClientSecret = new ClientSecret(TwitchApiTestFixture.TestClientSecret),
            Code = "invalid_code",
            RedirectUri = new Uri(TwitchApiTestFixture.TestRedirectUri)
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<TwitchApiException>(() => client.SendAsync(request).AsTask());
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Fact]
    public async Task SendAsync_MissingClientId_ThrowsTwitchApiException()
    {
        // Arrange - Using HttpClient directly since we can't construct request without required ClientId
        var httpClient = _fixture.CreateClient();
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_secret", TwitchApiTestFixture.TestClientSecret },
            { "code", TwitchApiTestFixture.TestAuthorizationCode },
            { "grant_type", "authorization_code" },
            { "redirect_uri", TwitchApiTestFixture.TestRedirectUri }
        });

        // Act
        var response = await httpClient.PostAsync("/oauth2/token", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task SendAsync_MissingClientSecret_ThrowsTwitchApiException()
    {
        // Arrange
        var httpClient = _fixture.CreateClient();
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", TwitchApiTestFixture.TestClientId },
            { "code", TwitchApiTestFixture.TestAuthorizationCode },
            { "grant_type", "authorization_code" },
            { "redirect_uri", TwitchApiTestFixture.TestRedirectUri }
        });

        // Act
        var response = await httpClient.PostAsync("/oauth2/token", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task SendAsync_MissingCode_ThrowsTwitchApiException()
    {
        // Arrange
        var httpClient = _fixture.CreateClient();
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", TwitchApiTestFixture.TestClientId },
            { "client_secret", TwitchApiTestFixture.TestClientSecret },
            { "grant_type", "authorization_code" },
            { "redirect_uri", TwitchApiTestFixture.TestRedirectUri }
        });

        // Act
        var response = await httpClient.PostAsync("/oauth2/token", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
