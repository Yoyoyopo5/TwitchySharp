using TwitchySharp.EventSub.Websocket.Clients;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Tests.Unit;
using TwitchySharp.Tests.Unit.Toolkit;

namespace TwitchySharp.EventSub.Websocket.Tests.Unit.Clients;

public class Test_WithReconnects
{
    private readonly static EventSubMessageMetadata _stubMetadata = new()
    {
        MessageId = new("12345"),
        MessageType = WebsocketMessageType.Welcome,
        MessageTimestamp = new(2026, 6, 22, 6, 43, 11, TimeSpan.Zero)
    };

    private const string MOCK_RECONNECT_URL = "wss://new-url.com";

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
                        KeepaliveTimeout = TimeSpan.FromSeconds(5),
                        ConnectedAt = DateTimeOffset.MinValue
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
        public Func<string, CancellationToken, Task>? OnMessage { get; set; }
        public bool Connected { get; private set; } = false;

        public Task Receive(string message, CancellationToken ct)
            => OnMessage is null ? Task.CompletedTask : OnMessage(message, ct);

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
            ws.OnMessage = (message, ct) => pipeline(new(message.ToMemoryStream()), ct).AsTask();
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

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(2));
        CancellationToken ct = cts.Token;

        await reconnectListen(_stubProcess, new("wss://original-url.com"), ct);

        await stubProvider.Websockets[0].Receive("reconnect", ct);
        while(!ct.IsCancellationRequested && stubProvider.Websockets.Count != 2)
        {
            await Task.Delay(100, ct);
        }

        Assert.Equal(2, stubProvider.Websockets.Count);
        Assert.Equal(new Uri(MOCK_RECONNECT_URL).AbsoluteUri, stubProvider.Websockets[1].Url?.AbsoluteUri);
    }

    [Fact]
    public async Task Listen_ThenReconnect_BothClientsRunning()
    {
        FakeWebsocketProvider stubProvider = new();
        StartEventSubWebsocketClient reconnectListen = stubProvider.MockStart.WithReconnects();

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(2));
        CancellationToken ct = cts.Token;

        await reconnectListen(_stubProcess, new("wss://original-url.com"), ct);

        await stubProvider.Websockets[0].Receive("welcome", ct); // promotes
        await stubProvider.Websockets[0].Receive("reconnect", ct);

        while (!ct.IsCancellationRequested
            && (stubProvider.Websockets.Count != 2
            || stubProvider.Websockets.Any(ws => !ws.Connected)))
        {
            await Task.Delay(100, ct);
        }

        Assert.Equal(2, stubProvider.Websockets.Count);
        Assert.All(stubProvider.Websockets, ws => Assert.True(ws.Connected));
    }

    [Fact]
    public async Task Listen_ThenReconnectAndWelcome_FirstClientStopped()
    {
        FakeWebsocketProvider stubProvider = new();
        StartEventSubWebsocketClient reconnectListen = stubProvider.MockStart.WithReconnects();

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(2));
        CancellationToken ct = cts.Token;

        await reconnectListen(_stubProcess, new("wss://original-url.com"), ct);

        await stubProvider.Websockets[0].Receive("welcome", ct);
        await stubProvider.Websockets[0].Receive("reconnect", ct);
        await Task.Delay(100, ct);

        await stubProvider.Websockets[1].Receive("welcome", ct);

        while(!ct.IsCancellationRequested && stubProvider.Websockets.First().Connected)
        {
            await Task.Delay(100, ct);
        }

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

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(2));
        CancellationToken ct = cts.Token;

        await reconnectListen(_stubProcess, new("wss://original-url.com"), TestContext.Current.CancellationToken);

        await stubProvider.Websockets[0].Receive("welcome", ct);
        await Concurrency.RunConcurrently(RECONNECT_COUNT, i => stubProvider.Websockets[0].Receive("reconnect", ct), ct);

        while (!ct.IsCancellationRequested
            && (stubProvider.Websockets.Count != RECONNECT_COUNT + 1
            || stubProvider.Websockets.Count(ws => ws.Connected) != 2))
        {
            await Task.Delay(100, ct);
        }

        Assert.Equal(RECONNECT_COUNT + 1, stubProvider.Websockets.Count);
        Assert.Equal(2, stubProvider.Websockets.Count(ws => ws.Connected));
    }

    [Fact]
    public async Task Listen_ThenMultipleReconnects_ThenWelcome_OnlyOneConnected()
    {
        const int RECONNECT_COUNT = 64;

        FakeWebsocketProvider stubProvider = new();
        StartEventSubWebsocketClient reconnectListen = stubProvider.MockStart.WithReconnects();

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(2));
        CancellationToken ct = cts.Token;

        await reconnectListen(_stubProcess, new("wss://original-url.com"), ct);

        await stubProvider.Websockets[0].Receive("welcome", ct);

        await Concurrency.RunConcurrently(RECONNECT_COUNT, i => stubProvider.Websockets[0].Receive("reconnect", ct), ct);

        foreach (FakeWebsocket ws in stubProvider.Websockets)
        {
            await ws.Receive("welcome", ct);
        }

        while (!ct.IsCancellationRequested
            && (stubProvider.Websockets.Count != RECONNECT_COUNT + 1
            || stubProvider.Websockets.Count(ws => ws.Connected) != 1))
        {
            await Task.Delay(100, TestContext.Current.CancellationToken);
        }

        Assert.Equal(1, stubProvider.Websockets.Count(ws => ws.Connected));
    }
}
