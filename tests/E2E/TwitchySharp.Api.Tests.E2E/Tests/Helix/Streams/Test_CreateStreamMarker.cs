using TwitchySharp.Api.Helix.Streams;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Streams;

[Collection("twitch")]
public class Test_CreateStreamMarker(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_CreateStreamMarkerRequest_ReturnSuccessResponse()
    {
        // Stream must be live, and VODs must be enabled to test this.

        CreateStreamMarkerRequest request = new()
        {
            Marker = new()
            {
                UserId = _fixture.UserIdentity.UserId,
                Description = "test marker"
            }
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
