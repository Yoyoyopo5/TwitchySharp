using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Subscriptions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Subscriptions;
[Collection("helix")]
public class Test_CheckUserSubscription(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_CheckUserSubscriptionRequest_ReturnSuccessResponse()
    {
        // I dislike how this endpoint uses an error code to indicate non-subscription status.
        // We should not encourage using exceptions for control flow, but I don't see a much better option at the moment.
        // If discriminated unions are added to C#, we could use those, or we could wrap every response in a monad object.
        // Both of these solutions will likely add significant pattern matching boilerplate for consumers, even when error codes indicate exceptionality.

        string broadcasterId = "52137752";
        string userId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new CheckUserSubscriptionRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            userId
            ));
    }
}
