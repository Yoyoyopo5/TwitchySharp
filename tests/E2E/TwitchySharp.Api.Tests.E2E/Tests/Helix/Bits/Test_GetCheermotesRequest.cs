using TwitchySharp.Api.Helix.Bits;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Bits;

[Collection("twitch")]
public class Test_GetCheermotesRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetCheermotesRequest_ReturnSuccessResponse()
    {
        const string BROADCASTER_ID = "52137752";
        UserId broadcasterId = new(BROADCASTER_ID);
        GetCheermotesRequest request = new()
        {
            BroadcasterId = broadcasterId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
