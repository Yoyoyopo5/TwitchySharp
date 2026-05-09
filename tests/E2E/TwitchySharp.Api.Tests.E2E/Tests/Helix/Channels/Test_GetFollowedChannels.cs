using TwitchySharp.Api.Helix.Channels;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

[Collection("twitch")]
public class Test_GetFollowedChannels(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetFollowedChannelsRequest_ReturnSuccessResponse()
    {
        GetFollowedChannelsRequest request = new()
        {
            UserId = _fixture.UserIdentity.UserId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
