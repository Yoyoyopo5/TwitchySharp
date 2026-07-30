using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_AddRemoveModerator(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("add-remove-moderator");

    [Fact]
    public async Task Send_AddRemoveModeratorRequests_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string TEST_MODERATOR_ID = "159571771";
        UserId moderatorId = new(TEST_MODERATOR_ID);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await AddChannelModerator(client, userConfig.UserId, moderatorId, ct);
        await Task.Delay(250, ct);
        await RemoveChannelModerator(client, userConfig.UserId, moderatorId, ct);
    }

    private static Task<TwitchResponse<AddChannelModeratorResponse>> AddChannelModerator(ITwitchClient client, UserId broadcasterId, UserId moderatorId, CancellationToken ct)
        => client.SendAsync(new AddChannelModeratorRequest()
        {
            BroadcasterId = broadcasterId,
            UserId = moderatorId
        }, ct);

    private static Task<TwitchResponse<RemoveChannelModeratorResponse>> RemoveChannelModerator(ITwitchClient client, UserId broadcasterId, UserId moderatorId, CancellationToken ct)
        => client.SendAsync(new RemoveChannelModeratorRequest()
        {
            BroadcasterId = broadcasterId,
            UserId = moderatorId
        }, ct);
}
