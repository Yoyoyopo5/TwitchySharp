using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_CheckAutoModStatus(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("check-auto-mod-status");

    [Fact]
    public async Task Send_CheckAutomodStatusRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        CheckAutoModStatusRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            Messages = new()
            {
                Messages = [ new() {
                    MessageId = "1",
                    MessageText = "test message"
                } ]
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
