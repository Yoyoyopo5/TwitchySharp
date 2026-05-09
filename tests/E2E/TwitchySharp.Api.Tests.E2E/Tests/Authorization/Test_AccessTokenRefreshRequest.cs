using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authorization;

[Collection("twitch")]
public class Test_AccessTokenRefreshRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_AccessTokenRefreshRequest_ReturnSuccessResponse()
    {
        using TokenStoreAcquisition tokens = await TwitchClientFixture.AcquireTokenStore(TestContext.Current.CancellationToken);
        AccessTokenDetails.User? userTokenDetails = tokens.Store.Values.OfType<AccessTokenDetails.User>().FirstOrDefault();
        Assert.NotNull(userTokenDetails);
        Assert.NotNull(userTokenDetails.RefreshToken);

        AccessTokenRefreshRequest request = new()
        {
            ClientId = new ClientId(TwitchClientFixture.ClientConfig.ClientId),
            ClientSecret = new ClientSecret(TwitchClientFixture.ClientConfig.ClientSecret),
            RefreshToken = userTokenDetails.RefreshToken.Value
        };

        ITwitchClient client = TwitchClientFixture.Client;
        var response = (await client.SendAsync(request, TestContext.Current.CancellationToken)).Content;
        AccessTokenDetails.User refreshedTokenDetails = userTokenDetails with
        {
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken,
            Scopes = response.Scope?.ToHashSet() ?? [],
            ExpiresAt = DateTimeOffset.UtcNow + response.ExpiresIn
        };
        tokens.Store.AddOrUpdate(userTokenDetails.Identity, refreshedTokenDetails, (_, _) => refreshedTokenDetails);

        Assert.Equal([.. userTokenDetails.Scopes], response.Scope?.ToHashSet());
        Assert.NotEqual(userTokenDetails.AccessToken, response.AccessToken);
        Assert.False(string.IsNullOrWhiteSpace(refreshedTokenDetails.RefreshToken));
        Assert.True(refreshedTokenDetails.ExpiresAt > userTokenDetails.ExpiresAt);
    }
}
