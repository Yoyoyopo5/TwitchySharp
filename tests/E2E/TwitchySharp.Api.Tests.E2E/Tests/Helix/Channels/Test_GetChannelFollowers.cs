using TwitchySharp.Api.Helix.Channels;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

[Collection("twitch")]
public class Test_GetChannelFollowers(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetChannelFollowersRequest_ReturnSuccessResponse()
    {
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;
        GetChannelFollowersRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            First = new(10)
        };

        var response = await client.SendAsync(request, ct);
        if (response.Content.Pagination.Cursor is not PaginationCursor cursor)
            return;

        // Also test pagination here
        await client.SendAsync(request with { After = cursor }, ct);
    }
}
