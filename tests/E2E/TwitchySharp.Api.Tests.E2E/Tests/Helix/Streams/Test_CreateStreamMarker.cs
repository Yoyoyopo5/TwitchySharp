using TwitchySharp.Api.Helix.Streams;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Streams;

public class Test_CreateStreamMarker(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("create-stream-marker");

    [Fact]
    public async Task Send_CreateStreamMarkerRequest_ReturnSuccessResponse()
    {
        // Stream must be live, and VODs must be enabled to test this.
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await client.SkipIfBroadcasterIsNotStreaming(userConfig.UserId, ct);

        CreateStreamMarkerRequest request = new()
        {
            Marker = new()
            {
                UserId = userConfig.UserId,
                Description = "test marker"
            }
        };

        await client.SendAsync(request, ct);
    }
}
