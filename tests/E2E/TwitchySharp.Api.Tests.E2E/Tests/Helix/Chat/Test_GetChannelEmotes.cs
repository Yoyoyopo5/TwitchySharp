using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_GetChannelEmotes(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-channel-emotes");

    [Fact]
    public async Task Send_GetChannelEmotesRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        UserId broadcasterId = new("52137752");

        GetChannelEmotesRequest request = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            BroadcasterId = broadcasterId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
