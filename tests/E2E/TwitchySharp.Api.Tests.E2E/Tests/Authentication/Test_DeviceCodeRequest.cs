using TwitchySharp.Api.Authorization;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authorization;

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
        ITwitchClient client = _fixture.GetTwitchApiClient();

        TwitchResponse<DeviceCodeResponse> response = await client.SendAsync(request, TestContext.Current.CancellationToken);

        Assert.False(string.IsNullOrEmpty(response.Content.DeviceCode.Value));
        Assert.True(response.Content.ExpiresIn > TimeSpan.Zero);
        Assert.True(response.Content.Interval > TimeSpan.Zero);
        Assert.False(string.IsNullOrEmpty(response.Content.UserCode));
        Assert.False(string.IsNullOrEmpty(response.Content.VerificationUri.AbsoluteUri));
    }
}
