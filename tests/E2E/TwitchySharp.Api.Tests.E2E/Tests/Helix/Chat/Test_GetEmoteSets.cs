using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

[Collection("twitch")]
public class Test_GetEmoteSets(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetEmoteSetsRequest_ReturnSuccessResponse()
    {
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;
        GetChannelEmotesRequest getEmotesRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        var getEmotesResponse = await client.SendAsync(getEmotesRequest, ct);
        if (getEmotesResponse.Content.Data.FirstOrDefault()?.EmoteSetId is not EmoteSetId id)
            return;

        GetEmoteSetsRequest request = new()
        {
            EmoteSetIds = [id]
        };

        await client.SendAsync(request, ct);
    }
}
