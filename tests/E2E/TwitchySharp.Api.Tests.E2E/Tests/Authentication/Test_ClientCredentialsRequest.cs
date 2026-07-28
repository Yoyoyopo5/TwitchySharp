using TwitchySharp.Api.Authorization;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authentication;

public class Test_ClientCredentialsRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private readonly static TestName TestName = new("client-credentials");

    [Fact]
    public async Task Send_ClientCredentialsRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        ClientCredentialsRequest request = new()
        {
            ClientId = new(clientConfig.ClientId),
            ClientSecret = new(clientConfig.ClientSecret)
        };
        ITwitchClient client = _fixture.GetTwitchApiClient();
        TwitchResponse<ClientCredentialsResponse> response = await client.SendAsync(request, TestContext.Current.CancellationToken);

        Assert.False(string.IsNullOrEmpty(response.Content.AccessToken.Value));
        Assert.Equal("bearer", response.Content.TokenType);
        Assert.True(response.Content.ExpiresIn > TimeSpan.Zero);
    }
}
