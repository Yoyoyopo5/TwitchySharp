using TwitchySharp.Api.Helix.Subscriptions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Subscriptions;

public class Test_GetBroadcasterSubscriptions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-broadcaster-subscriptions");

    [Fact]
    public async Task Send_GetBroadcasterSubscriptionsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetBroadcasterSubscriptionsRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
