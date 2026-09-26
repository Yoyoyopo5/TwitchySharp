using TwitchySharp.Api.Helix.Users;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Users;

public class Test_BlockUnblockUser(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("block-unblock-user");

    [Fact]
    public async Task Send_BlockUnblockUserRequests_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string TEST_USER_ID = "12345";
        UserId userToBlock = new(TEST_USER_ID);
        UserId userId = userConfig.UserId;
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await BlockUser(client, userId, userToBlock, ct);
        await Task.Delay(250, ct);
        await UnblockUser(client, userId, userToBlock, ct);
    }

    private static Task<TwitchResponse<BlockUserResponseContent>> BlockUser(TestingTwitchClient client, UserId userId, UserId blockUserId, CancellationToken ct)
        => client.SendAsync(new BlockUserRequest()
        {
            TargetUserId = blockUserId,
            UserId = userId
        }, TestName, ct);

    private static Task<TwitchResponse<UnblockUserResponseContent>> UnblockUser(TestingTwitchClient client, UserId userId, UserId blockedUserId, CancellationToken ct)
        => client.SendAsync(new UnblockUserRequest()
        {
            UserId = userId,
            TargetUserId = blockedUserId
        }, TestName, ct);
}
