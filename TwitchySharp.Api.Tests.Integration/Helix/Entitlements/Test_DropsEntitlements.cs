using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Entitlements;

namespace TwitchySharp.Api.Tests.Integration.Helix.Entitlements;
[Collection("helix")]
public class Test_DropsEntitlements(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_DropsEntitlementRequests_ReturnSuccessResponses()
    {
        string? entitlementId = (await GetDropsEntitlements()).Data.FirstOrDefault()?.Id;
        // Can't really test the update API unless we can actually get a drops entitlement on an account...
        if (entitlementId is null) return;
        await UpdateDropsEntitlements(entitlementId);
    }

    private ValueTask<GetDropsEntitlementsResponse> GetDropsEntitlements()
        => _fixture.Api.SendRequestAsync(new GetDropsEntitlementsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken
            ));

    private ValueTask<UpdateDropsEntitlementsResponse> UpdateDropsEntitlements(string entitlementId)
        => _fixture.Api.SendRequestAsync(new UpdateDropsEntitlementsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new UpdateDropsEntitlementsRequestData()
            {
                EntitlementIds = [entitlementId],
                FulfillmentStatus = DropsEntitlementStatus.Fulfilled
            }
            ));
}
