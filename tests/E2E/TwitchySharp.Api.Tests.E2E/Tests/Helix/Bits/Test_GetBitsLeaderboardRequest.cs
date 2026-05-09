using TwitchySharp.Api.Helix.Bits;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Bits;

[Collection("twitch")]
public class Test_GetBitsLeaderboardRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetBitsLeaderboardRequest_ReturnSuccessResponse()
    {
        GetBitsLeaderboardRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
