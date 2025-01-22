using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Charity;

namespace TwitchySharp.Api.Tests.Integration.Helix.Charity;
[Collection("helix")]
public class Test_GetCharityCampaignDonations(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetCharityCampaignDonations_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new GetCharityCampaignDonationsRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.UserAccessToken,
            broadcasterId
            ));
    }
}
