using TwitchySharp.Api.Helix.Ads;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels.Ads;

public class Test_GetAdSchedule(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-ad-schedule");

    [Fact]
    public async Task Send_GetAdScheduleRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetAdScheduleRequest request = new()
        {
            BroadcasterId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
