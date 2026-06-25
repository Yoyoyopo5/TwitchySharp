using TwitchySharp.EventSub.Websocket.Clients;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Tests.Unit;

namespace TwitchySharp.EventSub.Websocket.Tests.Unit.Clients;

public class Test_WithReconnects
{
    private readonly static EventSubMessageMetadata _stubMetadata = new()
    {
        MessageId = new("12345"),
        MessageType = WebsocketMessageType.Welcome,
        MessageTimestamp = new(2026, 6, 22, 6, 43, 11, TimeSpan.Zero)
    };

    const string MOCK_RECONNECT_URL = "wss://new-url.com";

    private static readonly ProcessWebsocketMessage _stubProcess = async (stream, ct) =>
    {
        using StreamReader sr = new(stream);
        string m = await sr.ReadToEndAsync(ct);
        return m switch
        {
            "welcome" => new EventSubWebsocketMessage<WelcomeMessagePayload>()
            {
                Metadata = _stubMetadata,
                Payload = new()
                {
                    Session = new()
                    {
                        Id = new("12345"),
                        Status = EventSubSessionStatus.Connected,
                        KeepaliveTimeout = TimeSpan.FromSeconds(5)
                    }
                }
            },
            "reconnect" => new EventSubWebsocketMessage<ReconnectMessagePayload>()
            {
                Metadata = _stubMetadata,
                Payload = new()
                {
                    Session = new()
                    {
                        Id = new("12345"),
                        Status = EventSubSessionStatus.Reconnecting,
                        ConnectedAt = new(2026, 6, 22, 6, 45, 20, TimeSpan.Zero),
                        ReconnectUrl = new(MOCK_RECONNECT_URL)
                    }
                }
            },
            _ => throw new NotSupportedException("Invalid message stream value.")
        };
    };

    public class FakeWebsocket
    {
        public Uri? Url { get; private set; }
        public Action<string>? OnMessage { get; set; }
        public bool Connected { get; private set; } = false;

        public void Receive(string message)
            => OnMessage?.Invoke(message);

        public void Configure(Uri url)
            => Url = url;

        public void Start()
            => Connected = true;

        public void Stop()
            => Connected = false;
    }

    public class FakeWebsocketProvider
    {
        private readonly List<FakeWebsocket> _websockets = [];
        public IReadOnlyList<FakeWebsocket> Websockets => _websockets;
        private readonly object _l = new();

        public Task<StopWebsocketClient> StartNew(ProcessWebsocketMessage pipeline, EventSubWebsocketUrl url, CancellationToken ct)
        {
            FakeWebsocket ws = new();
            ws.Configure(url.ToUri().Match(e => throw new ArgumentException(), uri => uri));
            ws.Start();
            ws.OnMessage = message => _ = pipeline(new(message.ToMemoryStream()), TestContext.Current.CancellationToken).AsTask();
            lock (_l)
            {
                _websockets.Add(ws);
            }

            return Task.FromResult<StopWebsocketClient>(_ =>
            {
                ws.Stop();
                return Task.CompletedTask;
            });
        }

        public StartEventSubWebsocketClient MockStart => StartNew;
    }

    [Fact]
    public async Task Listen_ThenReconnect_StartCalledWithNewUrl()
    {
        FakeWebsocketProvider stubProvider = new();

        StartEventSubWebsocketClient reconnectListen = stubProvider.MockStart.WithReconnects();

        await reconnectListen(_stubProcess, new("wss://original-url.com"), TestContext.Current.CancellationToken);

        stubProvider.Websockets[0].Receive("reconnect");
        await Task.Delay(2, TestContext.Current.CancellationToken);

        Assert.Equal(2, stubProvider.Websockets.Count);
        Assert.Equal(new Uri(MOCK_RECONNECT_URL).AbsoluteUri, stubProvider.Websockets[1].Url?.AbsoluteUri);
    }

    [Fact]
    public async Task Listen_ThenReconnect_BothClientsRunning()
    {
        FakeWebsocketProvider stubProvider = new();
        StartEventSubWebsocketClient reconnectListen = stubProvider.MockStart.WithReconnects();
        await reconnectListen(_stubProcess, new("wss://original-url.com"), TestContext.Current.CancellationToken);

        stubProvider.Websockets[0].Receive("welcome"); // promotes
        stubProvider.Websockets[0].Receive("reconnect");
        await Task.Delay(1, TestContext.Current.CancellationToken);

        Assert.Equal(2, stubProvider.Websockets.Count);
        Assert.All(stubProvider.Websockets, ws => Assert.True(ws.Connected));
    }

    [Fact]
    public async Task Listen_ThenReconnectAndWelcome_FirstClientStopped()
    {
        FakeWebsocketProvider stubProvider = new();
        StartEventSubWebsocketClient reconnectListen = stubProvider.MockStart.WithReconnects();
        await reconnectListen(_stubProcess, new("wss://original-url.com"), TestContext.Current.CancellationToken);

        stubProvider.Websockets[0].Receive("welcome");
        stubProvider.Websockets[0].Receive("reconnect");
        await Task.Delay(250, TestContext.Current.CancellationToken);

        stubProvider.Websockets[1].Receive("welcome");
        await Task.Delay(1, TestContext.Current.CancellationToken);

        Assert.Collection(
            stubProvider.Websockets,
            ws => Assert.False(ws.Connected),
            ws => Assert.True(ws.Connected)
            );
    }

    [Fact]
    public async Task Listen_MultipleReconnects_OnlyTwoClientsRunning()
    {
        const int RECONNECT_COUNT = 64;

        FakeWebsocketProvider stubProvider = new();
        StartEventSubWebsocketClient reconnectListen = stubProvider.MockStart.WithReconnects();
        await reconnectListen(_stubProcess, new("wss://original-url.com"), TestContext.Current.CancellationToken);

        stubProvider.Websockets[0].Receive("welcome");
        Parallel.For(0, RECONNECT_COUNT, _ => stubProvider.Websockets[0].Receive("reconnect"));
        await Task.Delay(2, TestContext.Current.CancellationToken);

        Assert.Equal(RECONNECT_COUNT + 1, stubProvider.Websockets.Count);
        Assert.Equal(2, stubProvider.Websockets.Count(ws => ws.Connected));
    }

    [Fact]
    public async Task Listen_ThenMultipleReconnects_ThenWelcome_OnlyOneConnected()
    {
        const int RECONNECT_COUNT = 64;

        FakeWebsocketProvider stubProvider = new();
        StartEventSubWebsocketClient reconnectListen = stubProvider.MockStart.WithReconnects();
        await reconnectListen(_stubProcess, new("wss://original-url.com"), TestContext.Current.CancellationToken);

        stubProvider.Websockets[0].Receive("welcome");
        Parallel.For(0, RECONNECT_COUNT, _ => stubProvider.Websockets[0].Receive("reconnect"));
        await Task.Delay(2, TestContext.Current.CancellationToken);

        foreach (FakeWebsocket ws in stubProvider.Websockets)
        {
            ws.Receive("welcome");
        }
        await Task.Delay(2, TestContext.Current.CancellationToken);

        Assert.Equal(1, stubProvider.Websockets.Count(ws => ws.Connected));
    }
}
