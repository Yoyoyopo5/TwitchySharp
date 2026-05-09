using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_AddRemoveModerator(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_AddRemoveModeratorRequests_ReturnSuccessResponses()
    {
        const string TEST_MODERATOR_ID = "52137750";
        UserId moderatorId = new(TEST_MODERATOR_ID);
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        await AddChannelModerator(client, _fixture.UserIdentity.UserId, moderatorId, ct);
        await Task.Delay(250, ct);
        await RemoveChannelModerator(client, _fixture.UserIdentity.UserId, moderatorId, ct);
    }

    private static ValueTask<TwitchResponse<AddChannelModeratorResponse>> AddChannelModerator(ITwitchClient client, UserId broadcasterId, UserId moderatorId, CancellationToken ct)
        => client.SendAsync(new AddChannelModeratorRequest()
        {
            BroadcasterId = broadcasterId,
            UserId = moderatorId
        }, ct);

    private static ValueTask<TwitchResponse<RemoveChannelModeratorResponse>> RemoveChannelModerator(ITwitchClient client, UserId broadcasterId, UserId moderatorId, CancellationToken ct)
        => client.SendAsync(new RemoveChannelModeratorRequest()
        {
            BroadcasterId = broadcasterId,
            UserId = moderatorId
        }, ct);
}
