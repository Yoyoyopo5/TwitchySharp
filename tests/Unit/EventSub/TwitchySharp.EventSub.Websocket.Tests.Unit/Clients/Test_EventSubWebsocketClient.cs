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

        StartEventSubWebsocketClient startStubClient
            = EventSubWebsocketClient.Create(ctx => ct => Task.FromResult<StopWebsocketClient>(ct => Task.CompletedTask));

        await startStubClient(_stubProcess, new(VALID_URI), TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task ListenToEventSubWebsocketClient_InvalidUriString_ThrowsArgumentException()
    {
        const string VALID_URI = "invalid uri";

        StartEventSubWebsocketClient stubListen
            = EventSubWebsocketClient.Create(ctx => ct => Task.FromResult<StopWebsocketClient>(ct => Task.CompletedTask));

        await Assert.ThrowsAsync<ArgumentException>(async () => await stubListen(_stubProcess, new(VALID_URI), TestContext.Current.CancellationToken));
    }

    // -- Function Calling --

    [Fact]
    public async Task ListenToEventSubWebsocketClient_CallsCreateAndStart()
    {
        bool callsCreate = false;
        bool callsStart = false;

        StartEventSubWebsocketClient mockListen
            = EventSubWebsocketClient.Create(ctx =>
            {
                callsCreate = true;
                return ct =>
                {
                    callsStart = true;
                    return Task.FromResult<StopWebsocketClient>(ct => Task.CompletedTask);
                };
            });

        StopWebsocketClient stop = await mockListen(_stubProcess, new("wss://test.com"), TestContext.Current.CancellationToken);
        await stop(TestContext.Current.CancellationToken);

        Assert.True(callsCreate);
        Assert.True(callsStart);
    }

    [Fact]
    public async Task ListenToEventSubWebsocketClient_ThenStop_CallsStop()
    {
        bool callsStop = false;

        StartEventSubWebsocketClient mockListen
            = EventSubWebsocketClient.Create(ctx => ct => Task.FromResult<StopWebsocketClient>(ct =>
            {
                callsStop = true;
                return Task.CompletedTask;
            }));

        StopWebsocketClient stop = await mockListen(_stubProcess, new("wss://test.com"), TestContext.Current.CancellationToken);
        await stop(TestContext.Current.CancellationToken);

        Assert.True(callsStop);
    }
}
