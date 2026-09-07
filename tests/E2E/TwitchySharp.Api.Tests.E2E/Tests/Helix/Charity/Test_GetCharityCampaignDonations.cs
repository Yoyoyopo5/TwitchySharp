using TwitchySharp.Api.Helix.Charity;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Charity;

public class Test_GetCharityCampaignDonations(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-charity-campaign-donations");

    [Fact]
    public async Task Send_GetCharityCampaignDonations_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetCharityCampaignDonationsRequest request = new()
        {
            BroadcasterId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
