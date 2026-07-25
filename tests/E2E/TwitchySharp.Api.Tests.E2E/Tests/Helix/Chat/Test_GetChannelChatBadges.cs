using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_GetChannelChatBadges(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-channel-chat-badges");

    [Fact]
    public async Task Send_GetChannelChatBadgesRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        UserId broadcasterId = new("52137752");

        GetChannelChatBadgesRequest request = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            BroadcasterId = broadcasterId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
