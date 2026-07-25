using TwitchySharp.Api.Helix.Bits;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Bits;

public class Test_GetCheermotesRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-cheermotes");

    [Fact]
    public async Task Send_GetCheermotesRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        const string BROADCASTER_ID = "52137752";
        UserId broadcasterId = new(BROADCASTER_ID);
        GetCheermotesRequest request = new()
        {
            BroadcasterId = broadcasterId,
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
