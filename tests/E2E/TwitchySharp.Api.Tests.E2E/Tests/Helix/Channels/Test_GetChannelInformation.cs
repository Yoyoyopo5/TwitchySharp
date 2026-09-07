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
        _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        const string TEST_BROADCASTER_ID = "52137752";
        UserId testBroadcasterId = new(TEST_BROADCASTER_ID);
        GetChannelInformationRequest request = new()
        {
            BroadcasterIds = [testBroadcasterId]
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
