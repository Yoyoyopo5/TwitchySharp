using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

[Collection("twitch")]
public class Test_SendShoutout(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_SendShoutoutRequest_ReturnSuccessResponse()
    {
        const string TO_BROADCASTER_ID = "141879576"; // dreadbreadcrumb
        UserId toBroadcasterId = new(TO_BROADCASTER_ID);
        SendShoutoutRequest request = new()
        {
            FromBroadcasterId = _fixture.UserIdentity.UserId,
            ToBroadcasterId = toBroadcasterId,
            ModeratorId = _fixture.UserIdentity.UserId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
