using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

[Collection("twitch")]
public class Test_GetChannelEmotes(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetChannelEmotesRequest_ReturnSuccessResponse()
    {
        GetChannelEmotesRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
