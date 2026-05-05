using TwitchySharp.Api.Helix.GuestStar;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.GuestStar;

[Collection("twitch")]
public class Test_GetChannelGuestStarSettings(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetChannelGuestStarSettingsRequest_ReturnSuccessResponse()
    {
        GetChannelGuestStarSettingsRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            ModeratorId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
