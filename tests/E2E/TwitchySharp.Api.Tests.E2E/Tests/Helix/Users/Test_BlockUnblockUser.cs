using TwitchySharp.Api.Helix.Users;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Users;

[Collection("twitch")]
public class Test_BlockUnblockUser(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_BlockUnblockUserRequests_ReturnSuccessResponses()
    {
        const string TEST_USER_ID = "12345";
        UserId userToBlock = new(TEST_USER_ID);
        UserId userId = _fixture.UserIdentity.UserId;
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        await BlockUser(client, userId, userToBlock, ct);
        await Task.Delay(250, ct);
        await UnblockUser(client, userId, userToBlock, ct);
    }

    private static ValueTask<TwitchResponse<BlockUserResponse>> BlockUser(ITwitchClient client, UserId userId, UserId blockUserId, CancellationToken ct)
        => client.SendAsync(new BlockUserRequest()
        {
            TargetUserId = blockUserId,
            UserId = userId
        }, ct);

    private static ValueTask<TwitchResponse<UnblockUserResponse>> UnblockUser(ITwitchClient client, UserId userId, UserId blockedUserId, CancellationToken ct)
        => client.SendAsync(new UnblockUserRequest()
        {
            UserId = userId,
            TargetUserId = blockedUserId
        }, ct);
}
