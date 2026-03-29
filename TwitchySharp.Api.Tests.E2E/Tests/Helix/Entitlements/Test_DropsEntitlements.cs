using TwitchySharp.Api.Helix.Entitlements;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Entitlements;

[Collection("twitch")]
public class Test_DropsEntitlements(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_DropsEntitlementRequests_ReturnSuccessResponses()
    {
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;
        GetDropsEntitlementsRequest getRequest = new();

        if ((await client.SendAsync(getRequest, ct)).Content.Data.FirstOrDefault()?.Id is not DropsEntitlementId id)
            return; // Can't really test the update API unless we can actually get a drops entitlement on an account...

        UpdateDropsEntitlementsRequest updateRequest = new()
        {
            Updates = new()
            {
                EntitlementIds = [],
                FulfillmentStatus = DropsEntitlementStatus.Fulfilled
            }
        };

        await client.SendAsync(updateRequest, ct);
    }
}
