using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authorization;

[Collection("twitch")]
public class Test_ClientCredentialsRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_ClientCredentialsRequest_ReturnSuccessResponse()
    {
        ClientCredentialsRequest request = new()
        {
            ClientId = new(TwitchClientFixture.ClientConfig.ClientId),
            ClientSecret = new(TwitchClientFixture.ClientConfig.ClientSecret)
        };
        ITwitchClient client = TwitchClientFixture.Client;
        ClientCredentialsResponse response = (await client.SendAsync(request, TestContext.Current.CancellationToken)).Content;

        Assert.False(string.IsNullOrEmpty(response.AccessToken.Value));
        Assert.Equal("bearer", response.TokenType);
        Assert.True(response.ExpiresIn > TimeSpan.Zero);
    }
}
