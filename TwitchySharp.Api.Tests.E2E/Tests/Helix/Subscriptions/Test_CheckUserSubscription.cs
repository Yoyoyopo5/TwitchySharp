using TwitchySharp.Api.Helix.Subscriptions;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Subscriptions;

[Collection("twitch")]
public class Test_CheckUserSubscription(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_CheckUserSubscriptionRequest_ReturnSuccessResponse()
    {
        // I dislike how this endpoint uses an error code to indicate non-subscription status.
        // We should not encourage using exceptions for control flow, but I don't see a much better option at the moment.
        // If discriminated unions are added to C#, we could use those, or we could wrap every response in a monad object.
        // Both of these solutions will likely add significant pattern matching boilerplate for consumers, even when error codes indicate exceptionality.

        const string TEST_USER_ID = "141879576";
        UserId testUserId = new(TEST_USER_ID);

        CheckUserSubscriptionRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            UserId = testUserId,
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
