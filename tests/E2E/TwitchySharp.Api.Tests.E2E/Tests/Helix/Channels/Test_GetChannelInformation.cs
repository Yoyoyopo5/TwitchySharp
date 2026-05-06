using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

[Collection("twitch")]
public class Test_GetChannelInformation(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetChannelInformationRequest_ReturnSuccessResponse()
    {
        const string TEST_BROADCASTER_ID = "141879576"; // dreadbreadcrumb
        UserId testBroadcasterId = new(TEST_BROADCASTER_ID);
        GetChannelInformationRequest request = new()
        {
            BroadcasterIds = [ _fixture.UserIdentity.UserId, testBroadcasterId ]
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
