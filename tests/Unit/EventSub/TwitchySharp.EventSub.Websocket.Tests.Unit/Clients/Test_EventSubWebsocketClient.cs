using TwitchySharp.EventSub.Websocket.Clients;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Websocket.Tests.Unit.Clients;

public class Test_EventSubWebsocketClient
{
    private readonly ProcessWebsocketMessage _stubProcess = (_, _)
        => ValueTask.FromResult<Validation<EventSubWebsocketMessage>>(
            new EventSubWebsocketMessage()
            {
                Metadata = new()
                {
                    MessageId = new("12345"),
                    MessageType = WebsocketMessageType.Welcome,
                    MessageTimestamp = new DateTimeOffset(2026, 6, 22, 4, 55, 23, TimeSpan.Zero)
                }
            });

    // -- Uri --

    [Fact]
    public async Task ListenToEventSubWebsocketClient_ValidUriString_ReturnsTask()
    {
        const string VALID_URI = "wss://test-websocket.com";

        ListenToEventSubWebsocketClient stubListen
            = EventSubWebsocketClient.Create(ctx => ct => Task.FromResult<StopWebsocketClient>(() => Task.CompletedTask));

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(1);

        await stubListen(_stubProcess, new(VALID_URI), cts.Token);
    }

    [Fact]
    public async Task ListenToEventSubWebsocketClient_InvalidUriString_ThrowsArgumentException()
    {
        const string VALID_URI = "invalid uri";

        ListenToEventSubWebsocketClient stubListen
            = EventSubWebsocketClient.Create(ctx => ct => Task.FromResult<StopWebsocketClient>(() => Task.CompletedTask));

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(1);

        await Assert.ThrowsAsync<ArgumentException>(async () => await stubListen(_stubProcess, new(VALID_URI), cts.Token));
    }

    // -- Function Calling --

    [Fact]
    public async Task ListenToEventSubWebsocketClient_CallsCreateAndStart()
    {
        bool callsCreate = false;
        bool callsStart = false;

        ListenToEventSubWebsocketClient mockListen
            = EventSubWebsocketClient.Create(ctx =>
            {
                callsCreate = true;
                return ct =>
                {
                    callsStart = true;
                    return Task.FromResult<StopWebsocketClient>(() => Task.CompletedTask);
                };
            });

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(1);

        await mockListen(_stubProcess, new("wss://test.com"), cts.Token);

        Assert.True(callsCreate);
        Assert.True(callsStart);
    }

    [Fact]
    public async Task ListenToEventSubWebsocketClient_ThenCancelToken_CallsStop()
    {
        bool callsStop = false;

        ListenToEventSubWebsocketClient mockListen
            = EventSubWebsocketClient.Create(ctx => ct => Task.FromResult<StopWebsocketClient>(() =>
            {
                callsStop = true;
                return Task.CompletedTask;
            }));

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(1);

        await mockListen(_stubProcess, new("wss://test.com"), cts.Token);
        // Stopping is fire-and-forget, so we have to wait for the stopping logic to complete
        // before evaluating it here.
        await Task.Delay(1, TestContext.Current.CancellationToken);

        Assert.True(callsStop);
    }

    // -- Lifecycle --

    [Fact]
    public async Task ListenToEventSubWebsocketClient_WithCancelledToken_CancelsStart()
    {
        ListenToEventSubWebsocketClient mockListen
            = EventSubWebsocketClient.Create(ctx => ct =>
            {
                // This could also just return completed task,
                // the actual cancellation behavior is up to the consumer.
                ct.ThrowIfCancellationRequested();
                return Task.FromResult<StopWebsocketClient>(() => Task.CompletedTask);
            });

        CancellationTokenSource cts = new();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await mockListen(_stubProcess, new("wss://test.com"), cts.Token));
    }

    [Fact]
    public async Task ListenToEventSubWebsocketClient_ThenCancelToken_ReturnsCompletedTask()
    {
        ListenToEventSubWebsocketClient mockListen
            = EventSubWebsocketClient.Create(ctx => ct =>
            {
                // This could also just return completed task,
                // the actual cancellation behavior is up to the consumer.
                ct.ThrowIfCancellationRequested();
                return Task.FromResult<StopWebsocketClient>(() => Task.CompletedTask);
            });

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        cts.CancelAfter(1);

        // This should not throw, the listener is configured to complete the task
        // normally when cancellation is requested.
        await mockListen(_stubProcess, new("wss://test.com"), cts.Token);
    }

}
