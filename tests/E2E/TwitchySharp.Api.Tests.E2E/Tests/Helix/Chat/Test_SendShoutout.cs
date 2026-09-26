using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_SendShoutout(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("send-shoutout");

    [Fact]
    public async Task Send_SendShoutoutRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await client.SkipIfBroadcasterIsNotStreaming(userConfig.UserId, ct);

        const string TO_BROADCASTER_ID = "52137752";
        UserId toBroadcasterId = new(TO_BROADCASTER_ID);

        SendShoutoutRequest request = new()
        {
            FromBroadcasterId = userConfig.UserId,
            ToBroadcasterId = toBroadcasterId,
            ModeratorId = userConfig.UserId
        };

        await client.SendAsync(request, TestName, ct);
    }
}
