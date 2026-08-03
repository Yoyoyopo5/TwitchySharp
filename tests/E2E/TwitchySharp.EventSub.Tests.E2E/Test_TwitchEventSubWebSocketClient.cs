using TwitchySharp.EventSub.Websocket.Clients;
using TwitchySharp.EventSub.Websocket;

namespace TwitchySharp.EventSub.Tests.E2E;

public sealed class Test_TwitchEventSubWebSocketClient(EventSubWebsocketFixture fixture) : IAsyncLifetime
{
    private readonly EventSubWebsocketFixture _fixture = fixture;

    private StopWebsocketClient? _stopClient = null;
    private readonly TaskCompletionSource<EventSubWebsocketSession> _welcomeReceived = new();

    public async ValueTask InitializeAsync()
        => _stopClient = await _fixture.StartWebsocketClient(process =>
            process.MapWelcome((session, ct) =>
            {
                _welcomeReceived.TrySetResult(session);
                return ValueTask.CompletedTask;
            }),
            TestContext.Current.CancellationToken);

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

        await _welcomeReceived.Task.WaitAsync(cts.Token);
    }
}
