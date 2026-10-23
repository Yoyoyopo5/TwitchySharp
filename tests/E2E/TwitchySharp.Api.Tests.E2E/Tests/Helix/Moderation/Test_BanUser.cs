using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_BanUser(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("ban-user");

    [Fact]
    public async Task Send_BanUnbanUserRequests_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string TEST_BANNED_USER_ID = "52137750";
        UserId bannedUserId = new(TEST_BANNED_USER_ID);
        UserId broadcasterId = userConfig.UserId;
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        Assert.Null((await BanUser(client, broadcasterId, bannedUserId, ct)).Error);
        await Task.Delay(250, ct);
        Assert.Null((await UnbanUser(client, broadcasterId, bannedUserId, ct)).Error);
    }

    private static Task<TwitchResponse<BanUserResponseContent>> BanUser(TestingTwitchClient client, UserId broadcasterId, UserId userId, CancellationToken ct)
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
        }, TestName, ct);

    private static Task<TwitchResponse<UnbanUserResponseContent>> UnbanUser(TestingTwitchClient client, UserId broadcasterId, UserId userId, CancellationToken ct)
        => client.SendAsync(new UnbanUserRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            UserId = userId,
        }, TestName, ct);
}
