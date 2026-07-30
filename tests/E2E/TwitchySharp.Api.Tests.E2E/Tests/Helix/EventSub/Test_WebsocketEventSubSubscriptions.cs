using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Subscriptions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

public class Test_WebsocketEventSubSubscriptions(EventSubWebSocketsFixture fixture) : IClassFixture<EventSubWebSocketsFixture>
{
    private readonly EventSubWebSocketsFixture _fixture = fixture;

    [Fact]
    public async Task WebsocketEventSubCreateGetDelete()
    {
        EventSubWebsocketSessionId sessionId = new(_fixture.SessionId);

        await _fixture.SendEventSubSubscriptionRequests(
            EventSubTestRegistry.Get<ChannelFollow>(),
            new WebsocketSubscriptionTransport(sessionId),
            TestContext.Current.CancellationToken
            );
    }
}
