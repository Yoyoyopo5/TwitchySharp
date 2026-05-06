using TwitchySharp.Api.Helix.Ads;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels.Ads;

[Collection("twitch")]
public class Test_StartCommercial(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_StartCommercialRequest_ReturnSuccessResponse()
    {
        // This likely requires the broadcaster to be live to return success.
        StartCommercialRequest request = new()
        {
            Commercial = new()
            {
                BroadcasterId = _fixture.UserIdentity.UserId,
                Length = TimeSpan.FromSeconds(30)
            }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
