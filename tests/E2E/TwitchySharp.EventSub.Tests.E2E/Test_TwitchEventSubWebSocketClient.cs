using TwitchySharp.EventSub.Websocket.Clients;

namespace TwitchySharp.EventSub.Tests.E2E;

public sealed class Test_TwitchEventSubWebSocketClient(EventSubWebsocketFixture fixture) : IAsyncLifetime
{
    private readonly EventSubWebsocketFixture _fixture = fixture;

    private readonly TestHandler _handler = new();
    private StopWebsocketClient? _stopClient = null;

    public async ValueTask InitializeAsync()
        => _stopClient = await _fixture.StartWebsocketClient(_handler, TestContext.Current.CancellationToken);

    public async ValueTask DisposeAsync()
    {
        if (_stopClient is not null)
            await _stopClient(TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task WaitFor_WelcomeMessage()
    {
        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(1));

        await _handler.WaitForWelcome(cts.Token);
    }
}
