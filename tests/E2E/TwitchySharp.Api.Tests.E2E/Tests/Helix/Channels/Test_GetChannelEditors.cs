using TwitchySharp.Api.Helix.Channels;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

[Collection("twitch")]
public class Test_GetChannelEditors(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetChannelEditorsRequest_ReturnSuccessResponse()
    {
        GetChannelEditorsRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
