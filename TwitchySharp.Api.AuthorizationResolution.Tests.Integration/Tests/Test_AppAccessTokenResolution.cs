using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Tests;

public class Test_AppAccessTokenResolution(TokenResolutionTestFixture fixture)
    : IClassFixture<TokenResolutionTestFixture>
{
    private readonly TokenResolutionTestFixture _fixture = fixture;

    private AppAccessTokenResolutionOptions CreateAppTokenOptions(AccessTokenDetails.App? cachedToken, Action<AccessTokenDetails.App> newToken)
        => new()
        {
            AuthenticationClient = _fixture.CreateTestAuthenticationClient(),
            ClientSecretResolver = (_, _) => ValueTask.FromResult<ClientSecret?>(TokenResolutionTestFixture.ClientSecret),
            GetCachedToken = (_, _) => ValueTask.FromResult(cachedToken),
            OnNewToken = (token, _) =>
            {
                newToken(token);
                return ValueTask.CompletedTask;
            }
        };

    private static async ValueTask<TwitchResponse<TestTwitchResponseData>> SendTestRequest(ITwitchClient client)
        => await client.SendAsync(new TestAuthorizedTwitchRequest()
        {
            AuthorizationContext = new() { Identity = new TwitchIdentity.Client(new("fake_client_id")) }
        });

    [Fact]
    public async Task SendRequest_WithUnavailableCachedToken_NewTokenCreatedAndUsed()
    {
        AccessTokenDetails.App? newToken = null;
        TwitchAuthorizationResolutionOptions options = new TwitchAuthorizationResolutionOptions()
            .ConfigureIdentityTokenResolution(CreateAppTokenOptions(null, token => newToken = token));
        ITwitchClient twitchClient = _fixture.CreateTestClient(options);

        TwitchResponse<TestTwitchResponseData> response = await SendTestRequest(twitchClient);

        Assert.NotNull(newToken);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, newToken.AccessToken.Value);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, response.Content.RequestAuthorizationHeaders.BearerToken?.Value);
    }

    [Fact]
    public async Task SendRequest_WithExpiredCachedToken_NewTokenCreatedAndUsed()
    {
        AccessTokenDetails.App? newToken = null;
        AccessTokenDetails.App expiredToken = new()
        {
            AccessToken = new("12345"),
            ExpiresAt = DateTimeOffset.MinValue,
            Identity = TokenResolutionTestFixture.ClientIdentity
        };
        TwitchAuthorizationResolutionOptions options = new TwitchAuthorizationResolutionOptions()
            .ConfigureIdentityTokenResolution(CreateAppTokenOptions(expiredToken, token => newToken = token));
        ITwitchClient twitchClient = _fixture.CreateTestClient(options);

        TwitchResponse<TestTwitchResponseData> response = await SendTestRequest(twitchClient);

        Assert.NotNull(newToken);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, newToken.AccessToken.Value);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, response.Content.RequestAuthorizationHeaders.BearerToken?.Value);
    }
}
