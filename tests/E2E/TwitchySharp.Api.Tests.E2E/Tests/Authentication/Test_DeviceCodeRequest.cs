using TwitchySharp.Api.Authentication;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authentication;

public class Test_DeviceCodeRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("device-code");

    [Fact]
    public async Task Send_DeviceCodeRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        HashSet<Scope> scopes = [];
        DeviceCodeRequest request = new()
        {
            ClientId = new(clientConfig.ClientId),
            Scopes = scopes
        };
        TestingTwitchClient client = _fixture.GetTwitchApiClient();

        TwitchResponse<DeviceCodeResponseContent> response = await client.SendAsync(request, TestName, TestContext.Current.CancellationToken);

        Assert.False(string.IsNullOrEmpty(response.Content.DeviceCode.Value));
        Assert.True(response.Content.ExpiresIn > TimeSpan.Zero);
        Assert.True(response.Content.Interval > TimeSpan.Zero);
        Assert.False(string.IsNullOrEmpty(response.Content.UserCode));
        Assert.False(string.IsNullOrEmpty(response.Content.VerificationUri.AbsoluteUri));
    }
}
