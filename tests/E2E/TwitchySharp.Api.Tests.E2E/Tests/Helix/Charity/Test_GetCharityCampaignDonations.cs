using TwitchySharp.Api.Helix.Charity;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Charity;

[Collection("twitch")]
public class Test_GetCharityCampaignDonations(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetCharityCampaignDonations_ReturnSuccessResponse()
    {
        GetCharityCampaignDonationsRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
