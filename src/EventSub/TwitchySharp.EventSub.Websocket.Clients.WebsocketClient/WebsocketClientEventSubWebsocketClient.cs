using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;
using Websocket.Client;

namespace TwitchySharp.EventSub.Websocket.Clients.WebsocketDotClient;

internal interface IEventSubWebsocketClient
{
    static abstract IEventSubWebsocketClient Create();
    Task<Validation<IRunningEventSubWebsocketClient>> Start(CancellationToken ct);
}

internal interface IRunningEventSubWebsocketClient
{
    Task<Validation> Stop(CancellationToken ct);
}

internal interface IStoppedEventSubWebsocketClient
{

}

internal static class EventSubWebsocketClient
{
    public record WebsocketClientError(string Message, Exception Exception) : Error(Message);

    public static ListenToEventSubWebsocketClient Create(
        Func<WebsocketClient> clientFactory,
        ProcessWebsocketMessage pipeline,
        Func<CancellationToken> messageCancellationTokenFactory
        )
        => async ct =>
        {
            WebsocketClient client = clientFactory();
            IDisposable subscription = client.ConfigureWithPipeline(pipeline, messageCancellationTokenFactory);
            try
            {
                await client.StartOrFail(); // does not take cancellation token.
            }
            catch (Exception ex)
            {
                return new WebsocketClientError("Websocket client threw exception on start.", ex);
            }
            return (StopEventSubWebsocketClient)(async ct =>
            {
                try
                {
                    bool stopResult = await client.StopOrFail(WebSocketCloseStatus.NormalClosure, "Connection closed");
                    subscription.Dispose();
                    client.Dispose();
                    return stopResult;
                }
                catch (Exception ex)
                {
                    return new WebsocketClientError("Websocket client threw exception on stop.", ex);
                }
            });
        };

    public static Validation<Func<WebsocketClient>> CreateFactory(
        EventSubWebsocketUrl url,
        ILogger<WebsocketClient>? logger = null,
        Func<Uri, CancellationToken, Task<WebSocket>>? connectionFactory = null,
        RecyclableMemoryStreamManager? memoryStreamManager = null
        )
        => url.ToUri()
            .Map<Func<WebsocketClient>>(uri => () => new WebsocketClient(uri, logger, connectionFactory, memoryStreamManager));

    private static IDisposable ConfigureWithPipeline(
        this WebsocketClient client,
        ProcessWebsocketMessage pipeline,
        Func<CancellationToken> messageCancellationTokenFactory
        )
    {
        client.IsTextMessageConversionEnabled = false; // Disable text conversion, gives us raw stream access
        client.IsStreamDisposedAutomatically = false; // Disable disposing the receive stream, we will handle that
        IDisposable subscription = client.MessageReceived.Subscribe(onNext: CreateMessageProcessor(pipeline, messageCancellationTokenFactory));
        return subscription;
    }
    
    private static Action<ResponseMessage> CreateMessageProcessor(
        ProcessWebsocketMessage pipeline,
        Func<CancellationToken> cancellationTokenFactory
        )
        => async message =>
        {
            if (message.Stream is not Stream stream)
                return;

            CancellationToken ct = cancellationTokenFactory();
            await pipeline(new(stream), ct);
            await stream.DisposeAsync();
        };
}
