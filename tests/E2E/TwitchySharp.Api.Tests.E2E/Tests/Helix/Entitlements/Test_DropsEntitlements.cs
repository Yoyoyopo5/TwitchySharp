using TwitchySharp.Api.Helix.Entitlements;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Entitlements;

public class Test_DropsEntitlements(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("drops-entitlements");

    [Fact]
    public async Task Send_DropsEntitlementRequests_ReturnSuccessResponses()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetDropsEntitlementsRequest getRequest = new();

        if ((await client.SendAsync(getRequest, ct)).Content.Data.FirstOrDefault()?.Id is not DropsEntitlementId id)
        {
            Assert.Skip("There are no drops entitlements associated with the client.");
            return;
        }

        UpdateDropsEntitlementsRequest updateRequest = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            Updates = new()
            {
                EntitlementIds = [],
                FulfillmentStatus = DropsEntitlementStatus.Fulfilled
            }
        };

        await client.SendAsync(updateRequest, ct);
    }
}
