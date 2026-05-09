using TwitchySharp.Api.Helix.Streams;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Streams;

[Collection("twitch")]
public class Test_GetFollowedStreams(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetFollowedStreamsRequest_ReturnSuccessResponse()
    {
        GetFollowedStreamsRequest request = new()
        {
            UserId = _fixture.UserIdentity.UserId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
