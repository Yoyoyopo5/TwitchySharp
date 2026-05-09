using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_CheckAutoModStatus(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_CheckAutomodStatusRequest_ReturnSuccessResponse()
    {
        CheckAutoModStatusRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            Messages = new()
            {
                Messages = [ new() {
                    MessageId = "1",
                    MessageText = "test message"
                } ]
            }
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
