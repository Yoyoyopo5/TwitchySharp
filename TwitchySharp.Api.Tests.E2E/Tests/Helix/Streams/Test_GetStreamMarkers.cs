using TwitchySharp.Api.Helix.Streams;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Streams;

[Collection("twitch")]
public class Test_GetStreamMarkers(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetStreamMarkersRequest_ReturnSuccessResponse()
    {
        GetStreamMarkersRequest request = new()
        {
            Query = new BroadcasterStreamMarkersQuery()
            {
                UserId = _fixture.UserIdentity.UserId
            },
            UserId = _fixture.UserIdentity.UserId,
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
