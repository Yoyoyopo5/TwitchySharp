using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_GetEmoteSets(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-emote-sets");

    [Fact]
    public async Task Send_GetEmoteSetsRequest_ReturnSuccessResponse()
    {
        _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        UserId broadcasterId = new("52137752");

        GetChannelEmotesRequest getEmotesRequest = new()
        {
            BroadcasterId = broadcasterId
        };

        TwitchResponse<GetChannelEmotesResponseContent> getEmotesResponse = await client.SendAsync(getEmotesRequest, TestName, ct);
        if (getEmotesResponse.Content.Data.FirstOrDefault()?.EmoteSetId is not EmoteSetId id)
            return;

        GetEmoteSetsRequest request = new()
        {
            EmoteSetIds = [id]
        };

        await client.SendAsync(request, TestName, ct);
    }
}
