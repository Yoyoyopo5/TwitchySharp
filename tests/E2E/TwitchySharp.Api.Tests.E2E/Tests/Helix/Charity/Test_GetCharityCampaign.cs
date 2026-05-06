using TwitchySharp.Api.Helix.Charity;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Charity;

[Collection("twitch")]
public class Test_GetCharityCampaign(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetCharityCampaignRequest_ReturnSuccessResponse()
    {
        GetCharityCampaignRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
