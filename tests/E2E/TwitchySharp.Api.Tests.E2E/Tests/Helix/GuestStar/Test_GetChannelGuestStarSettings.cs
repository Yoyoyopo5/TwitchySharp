using TwitchySharp.Api.Helix.GuestStar;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.GuestStar;

public class Test_GetChannelGuestStarSettings(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-channel-guest-star-settings");

    [Fact]
    public async Task Send_GetChannelGuestStarSettingsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetChannelGuestStarSettingsRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
