using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

public class Test_GetChannelInformation(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private readonly static TestName TestName = new("get-channel-information");

    [Fact]
    public async Task Send_GetChannelInformationRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        const string TEST_BROADCASTER_ID = "141879576"; // dreadbreadcrumb
        UserId testBroadcasterId = new(TEST_BROADCASTER_ID);
        GetChannelInformationRequest request = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            BroadcasterIds = [testBroadcasterId]
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
