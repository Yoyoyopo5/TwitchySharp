using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_GetAutoModSettings(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-auto-mod-settings");

    [Fact]
    public async Task Send_GetAutoModSettingsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetAutoModSettingsRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
