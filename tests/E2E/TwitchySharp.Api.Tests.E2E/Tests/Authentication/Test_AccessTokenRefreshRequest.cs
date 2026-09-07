using TwitchySharp.Api.Authentication;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authentication;

public class Test_AccessTokenRefreshRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    private readonly static TestName TestName = new("access-token-refresh");

    [Fact]
    public async Task Send_AccessTokenRefreshRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

         if (_fixture.GetClientConfig(userConfig) is not ClientConfiguration clientConfig)
        {
            Assert.Skip($"Could not find a {typeof(ClientConfiguration).Name} with token ClientId {userConfig.Token.ClientId}.");
            return;
        }

        AccessTokenDetails.User userTokenDetails = userConfig.ToAccessTokenDetails();

        Assert.NotNull(userTokenDetails.RefreshToken);

        AccessTokenRefreshRequest request = new()
        {
            ClientId = new ClientId(clientConfig.ClientId),
            ClientSecret = new ClientSecret(clientConfig.ClientSecret),
            RefreshToken = userTokenDetails.RefreshToken.Value
        };

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        TwitchResponse<AccessTokenRefreshResponseContent> response = await client.SendAsync(request, TestName, TestContext.Current.CancellationToken);

        AccessTokenDetails.User refreshedTokenDetails = userTokenDetails with
        {
            AccessToken = response.Content.AccessToken,
            RefreshToken = response.Content.RefreshToken,
            Scopes = response.Content.Scope?.ToHashSet() ?? [],
            ExpiresAt = DateTimeOffset.UtcNow + response.Content.ExpiresIn
        };

        // Update the token store for other tests since this test will invalidate the existing token.
        _fixture.GetTokenStore().AddOrUpdate(refreshedTokenDetails);

        Assert.Equal([.. userTokenDetails.Scopes], response.Content.Scope?.ToHashSet());
        Assert.NotEqual(userTokenDetails.AccessToken, response.Content.AccessToken);
        Assert.False(string.IsNullOrWhiteSpace(refreshedTokenDetails.RefreshToken));
    }
}
