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
        // I dislike how this endpoint uses an error code to indicate non-subscription status.
        // We should not encourage using exceptions for control flow, but I don't see a much better option at the moment.
        // If discriminated unions are added to C#, we could use those, or we could wrap every response in a monad object.
        // Both of these solutions will likely add significant pattern matching boilerplate for consumers, even when error codes indicate exceptionality.

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string TEST_USER_ID = "141879576";
        UserId testUserId = new(TEST_USER_ID);

        CheckUserSubscriptionRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            UserId = testUserId,
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
