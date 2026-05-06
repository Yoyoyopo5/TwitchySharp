using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Text;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Websocket.Deserialization;
using TwitchySharp.EventSub.Websocket.Messages;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;
using TwitchySharp.Helpers.Interfaces;
using Websocket.Client;

namespace TwitchySharp.EventSub.Websocket.Clients.Websocket.Client;

public class WebsocketClientEventSubWebsocketClient(
    IWebsocketEventSubHandler eventSubHandler,
    IEventSubWebsocketMessageDeserializer? messageDeserializer = null,
    IWebsocketClientFactory? websocketClientFactory = null,
    ICancellationTokenFactory? messageCancellationTokenFactory = null
    ) : IHostedService, IDisposable
{
    private readonly IWebsocketEventSubHandler _handler = eventSubHandler;
    private readonly IWebsocketClientFactory _clientFactory = websocketClientFactory ?? new DefaultWebsocketClientFactory();
    private readonly IEventSubWebsocketMessageDeserializer _deserializer = messageDeserializer ?? new DefaultWebsocketMessageDeserializer();
    private readonly ICancellationTokenFactory? _cancellationTokenFactory = messageCancellationTokenFactory;

    private readonly ConcurrentBag<IWebsocketClient> _clients = [];

    private async ValueTask HandleMessage(ResponseMessage message, IWebsocketClient client, CancellationToken ct = default)
    {
        if (message.MessageType != WebSocketMessageType.Text)
        {
            await _handler.OnException(new NotSupportedException("Binary messages are not supported."), ct);
            return;
        }
        if (message.Text is null)
        {
            await _handler.OnException(new NotSupportedException("Message cannot be null."), ct);
            return;
        }

        // Jank because Websocket.Client does not expose string message types as Stream.
        Stream messageAsStream = new MemoryStream(Encoding.UTF8.GetBytes(message.Text));

        try
        {
            IEventSubWebsocketMessage deserializedMessage = await _deserializer.DeserializeMessage(messageAsStream, ct);
            ValueTask handlerTask = (deserializedMessage as EventSubWebsocketMessage<object>)?.Payload switch
            {
                WelcomeMessagePayload welcomePayload => WelcomeReceived(client, welcomePayload.Session, ct),
                KeepaliveMessagePayload keepalivePayload => _handler.OnKeepalive(ct),
                IEventSubNotification notificationPayload => _handler.OnNotified(notificationPayload, ct),
                ReconnectMessagePayload reconnectPayload => Reconnect(reconnectPayload.Session, ct),
                RevocationMessagePayload revocationPayload => _handler.OnSubscriptionRevoked(revocationPayload.Subscription, ct),
                _ => throw new NotSupportedException("Unsupported deserialized message type.")
            };
            await handlerTask;
        }
        catch (Exception ex)
        {
            await _handler.OnException(ex, ct);
        }
    }

    private void ConfigureClient(IWebsocketClient client)
        => client.MessageReceived
            .Where(message => !string.IsNullOrEmpty(message.Text))
            .Subscribe(
                async message => await HandleMessage(message, client, _cancellationTokenFactory?.CreateCancellationToken() ?? CancellationToken.None),
                async exception => await _handler.OnException(exception, _cancellationTokenFactory?.CreateCancellationToken() ?? CancellationToken.None)
                );

    private async ValueTask StartNewClient(IWebsocketClient client, CancellationToken ct = default)
    {
        ConfigureClient(client);
        await client.StartOrFail();
        _clients.Add(client);
    }

    private async ValueTask Reconnect(EventSubReconnectSession reconnectSession, CancellationToken ct = default)
    {
        IWebsocketClient newClient = _clientFactory.CreateWebsocketClient();
        newClient.Url = new Uri(reconnectSession.ReconnectUrl);
        await StartNewClient(newClient, ct);
        await _handler.OnReconnected(reconnectSession, ct);
    }

    private async ValueTask WelcomeReceived(IWebsocketClient client, EventSubWebsocketSession session, CancellationToken ct = default)
    {
        try
        {
            foreach (IWebsocketClient existingClient in _clients.Where(c => c != client))
            {
                existingClient.Dispose();
            }
            _clients.Clear();
        }
        finally
        {
            _clients.Add(client);
            await _handler.OnConnected(session, ct);
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (!_clients.IsEmpty)
            return;
        await StartNewClient(_clientFactory.CreateWebsocketClient(), cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        foreach (IWebsocketClient existingClient in _clients)
        {
            await existingClient.StopOrFail(WebSocketCloseStatus.NormalClosure, string.Empty);
            existingClient.Dispose();
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        foreach (IWebsocketClient existingClient in _clients)
        {
            existingClient.Dispose();
        }
    }
}
