using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.EventSub.Websocket.Functional;

namespace TwitchySharp.EventSub.Websocket;

public static class ProcessWebsocketMessageExtensions
{
    /// <summary>
    /// Configures the pipeline to notify an <see cref="IWebsocketEventSubHandler"/> when processing Websocket messages.
    /// </summary>
    /// <param name="pipeline">The pipeline to add a handler to.</param>
    /// <param name="handler">The handler to add.</param>
    /// <returns>A new <see cref="ProcessWebsocketMessage"/> configured to use the <paramref name="handler"/>.</returns>
    public static ProcessWebsocketMessage WithHandler(this ProcessWebsocketMessage pipeline, IWebsocketEventSubHandler handler)
        => pipeline.With(next => (message, ct) =>
            next(message, ct).MatchAsync(
                onError: async (error, ct) =>
                {
                    await handler.OnError(error, ct);
                    return new Validation<EventSubWebsocketMessage>(error);
                },
                onValid: async (message, ct) =>
                {
                    switch (message)
                    {
                        case EventSubWebsocketMessage<KeepaliveMessagePayload> keepalive:
                            await handler.OnKeepalive(ct);
                            break;
                        case EventSubWebsocketMessage<NotificationMessagePayload> notification:
                            await handler.OnNotified(notification.Payload.Notification, ct);
                            break;
                        case EventSubWebsocketMessage<WelcomeMessagePayload> welcome:
                            await handler.OnConnected(welcome.Payload.Session, ct);
                            break;
                        case EventSubWebsocketMessage<RevocationMessagePayload> revocation:
                            await handler.OnSubscriptionRevoked(revocation.Payload.Subscription, ct);
                            break;
                        case EventSubWebsocketMessage<ReconnectMessagePayload> reconnect:
                            await handler.OnReconnected(reconnect.Payload.Session, ct);
                            break;
                        default:
                            return new Error("Unsupported Websocket message type.");
                    }
                    return message;
                },
                ct
                )
            );
}
