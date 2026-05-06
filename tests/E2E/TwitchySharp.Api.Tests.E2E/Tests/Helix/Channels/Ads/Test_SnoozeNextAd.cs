using TwitchySharp.Api.Helix.Ads;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels.Ads;

[Collection("twitch")]
public class Test_SnoozeNextAd(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_SnoozeNextAdRequest_ReturnSuccessResponse()
    {
        // This may require an upcoming ad break to return success.
        SnoozeNextAdRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
