using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_BanUser(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_BanUnbanUserRequests_ReturnSuccessResponses()
    {
        const string TEST_BANNED_USER_ID = "52137750";
        UserId bannedUserId = new(TEST_BANNED_USER_ID);
        UserId broadcasterId = _fixture.UserIdentity.UserId;
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await BanUser(client, broadcasterId, bannedUserId, ct);
        await Task.Delay(250, ct);
        await UnbanUser(client, broadcasterId, bannedUserId, ct);
    }

    private static ValueTask<TwitchResponse<BanUserResponse>> BanUser(ITwitchClient client, UserId broadcasterId, UserId userId, CancellationToken ct)
        => client.SendAsync(new BanUserRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            Ban = new()
            {
                Data = new()
                {
                    UserId = userId,
                    Duration = TimeSpan.FromSeconds(120),
                    Reason = "test timeout"
                }
            }
        }, ct);

    private static ValueTask<TwitchResponse<UnbanUserResponse>> UnbanUser(ITwitchClient client, UserId broadcasterId, UserId userId, CancellationToken ct)
        => client.SendAsync(new UnbanUserRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            UserId = userId,
        }, ct);
}
