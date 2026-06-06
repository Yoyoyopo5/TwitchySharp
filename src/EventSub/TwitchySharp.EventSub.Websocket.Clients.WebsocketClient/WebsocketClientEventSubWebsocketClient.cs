using System.Reactive.Linq;
using Websocket.Client;

namespace TwitchySharp.EventSub.Websocket.Clients.WebsocketDotClient;

// Example
internal static class WebsocketDotClientEventSubWebsocketClient
{
    public static ListenToEventSubWebsocketClient Create()
    {
        return EventSubWebsocketClient.Create(
            createClient: ctx =>
            {
                WebsocketClient client = new(ctx.Uri)
                {
                    IsTextMessageConversionEnabled = false,
                    IsStreamDisposedAutomatically = false
                };

                IDisposable messageHandler = client.MessageReceived.Subscribe(message =>
                {
                    if (message.Stream is not Stream messageStream)
                        return;

                    CancellationTokenSource cts = new();
                    cts.CancelAfter(5000);

                    try
                    {
                        _ = ctx.OnMessage(messageStream, cts.Token).AsTask();
                    }
                    catch (TaskCanceledException)
                    {
                        // Could log or whatever.
                    }

                    messageStream.Dispose();
                });

                IDisposable disconnectHandler = client.DisconnectionHappened.Subscribe(disconnect =>
                {
                    ctx.OnError(new Exception("Websocket.Client lost connection."));
                    client.Dispose();
                });

                return async ct =>
                {
                    await client.StartOrFail();
                    return () =>
                    {
                        messageHandler.Dispose();
                        disconnectHandler.Dispose();
                        client.Dispose();
                        return Task.CompletedTask;
                    };
                };
            }
            );
    }
}
