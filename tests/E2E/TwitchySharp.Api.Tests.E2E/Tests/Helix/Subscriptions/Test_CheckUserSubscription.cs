using System.Net;
using TwitchySharp.Api.Helix.Subscriptions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Subscriptions;

public class Test_CheckUserSubscription(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("check-user-subscribed");

    [Fact]
    public async Task Send_CheckUserSubscriptionRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        UserId testBroadcasterId = new("52137752");

        CheckUserSubscriptionRequest request = new()
        {
            BroadcasterId = testBroadcasterId,
            UserId = userConfig.UserId,
        };

        try
        {
            await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
        }
        catch (TwitchApiException ex)
        {
            Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
        }
    }
}
