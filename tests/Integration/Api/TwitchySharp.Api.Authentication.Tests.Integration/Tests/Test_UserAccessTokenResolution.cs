using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Tests;

public class Test_UserAccessTokenResolution(TokenResolutionTestFixture fixture)
    : IClassFixture<TokenResolutionTestFixture>
{
    private readonly TokenResolutionTestFixture _fixture = fixture;

    private UserAccessTokenResolutionOptions CreateOptions(AccessTokenDetails.User? cachedToken, Action<AccessTokenDetails.User> newToken)
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

    private static ValueTask<TwitchResponse<TestTwitchResponseData>> SendTestRequest(ITwitchClient client)
        => client.SendAsync(new TestAuthorizedTwitchRequest()
        {
            AuthorizationContext = new() { Identity = TokenResolutionTestFixture.TestUserIdentity }
        });

    [Fact]
    public async Task SendRequest_WithExpiredCachedTokenWithRefreshToken_RefreshedTokenCreatedAndUsed()
    {
        AccessTokenDetails.User? newToken = null;
        AccessTokenDetails.User cachedToken = new()
        {
            ExpiresAt = DateTimeOffset.MinValue,
            AccessToken = new("expired_access_token"),
            Scopes = new HashSet<Scope>(),
            Identity = TokenResolutionTestFixture.TestUserIdentity,
            RefreshToken = TokenResolutionTestFixture.RefreshToken
        };
        TwitchAuthorizationResolutionOptions options = new TwitchAuthorizationResolutionOptions()
            .ConfigureIdentityTokenResolution(CreateOptions(cachedToken, token => newToken = token));
        ITwitchClient client = _fixture.CreateTestClient(options);

        TwitchResponse<TestTwitchResponseData> response = await SendTestRequest(client);

        Assert.NotNull(newToken);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, newToken.AccessToken.Value);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, response.Content.RequestAuthorizationHeaders.BearerToken?.Value);
    }

    [Fact]
    public async Task SendRequest_WithExpiredCachedTokenWithoutRefreshToken_ExpiredTokenUsed()
    {
        const string EXPIRED_TOKEN_VALUE = "expired_access_token";
        AccessTokenDetails.User? newToken = null;
        AccessTokenDetails.User cachedToken = new()
        {
            ExpiresAt = DateTimeOffset.MinValue,
            AccessToken = new(EXPIRED_TOKEN_VALUE),
            Scopes = new HashSet<Scope>(),
            Identity = TokenResolutionTestFixture.TestUserIdentity
        };
        TwitchAuthorizationResolutionOptions options = new TwitchAuthorizationResolutionOptions()
            .ConfigureIdentityTokenResolution(CreateOptions(cachedToken, token => newToken = token));
        ITwitchClient client = _fixture.CreateTestClient(options);

        TwitchResponse<TestTwitchResponseData> response = await SendTestRequest(client);

        Assert.Null(newToken);
        Assert.Equal(EXPIRED_TOKEN_VALUE, response.Content.RequestAuthorizationHeaders.BearerToken?.Value);
    }
}
