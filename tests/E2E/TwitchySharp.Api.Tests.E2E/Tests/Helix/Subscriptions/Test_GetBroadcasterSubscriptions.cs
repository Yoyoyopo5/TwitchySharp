using TwitchySharp.Api.Helix.Subscriptions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Subscriptions;

[Collection("twitch")]
public class Test_GetBroadcasterSubscriptions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetBroadcasterSubscriptionsRequest_ReturnSuccessResponse()
    {
        GetBroadcasterSubscriptionsRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
