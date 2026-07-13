using TwitchySharp.Api.Authorization;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authorization;

public class Test_AccessTokenRefreshRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    private readonly EndpointName _endpointName = new("refresh-access-token");

    [Fact]
    public async Task Send_AccessTokenRefreshRequest_ReturnSuccessResponse()
    {
        if (_fixture.GetUserConfigFor(_endpointName) is not UserConfiguration userConfig)
        {
            TestContext.Current.AddSkippedEndpointWarning(_endpointName);
            return;
        }
        ClientConfiguration clientConfig = _fixture.GetClientConfig();

        AccessTokenDetails.User userTokenDetails = userConfig.ToAccessTokenDetails(clientConfig.ClientId);

        Assert.NotNull(userTokenDetails.RefreshToken);

        AccessTokenRefreshRequest request = new()
        {
            ClientId = new ClientId(clientConfig.ClientId),
            ClientSecret = new ClientSecret(clientConfig.ClientSecret),
            RefreshToken = userTokenDetails.RefreshToken.Value
        };

        ITwitchClient client = _fixture.GetTwitchApiClient();
        TwitchResponse<AccessTokenRefreshResponse> response = await client.SendAsync(request, TestContext.Current.CancellationToken);

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
        Assert.True(refreshedTokenDetails.ExpiresAt > userTokenDetails.ExpiresAt);
    }
}
