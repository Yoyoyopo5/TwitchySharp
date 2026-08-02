using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.EventSub.Websocket.Clients;

namespace TwitchySharp.EventSub.Websocket;

public static class MessageHandlingExtensions
{
    /// <summary>
    /// Assign a function that is called when an <see cref="EventSubWebsocketMessage{T}"/> with payload type <typeparamref name="T"/> is received.
    /// </summary>
    /// <typeparam name="T">The payload type to call <paramref name="handleMessage"/> with.</typeparam>
    /// <param name="process">The process to assign the handler function to.</param>
    /// <param name="handleMessage">The function to call when an <see cref="EventSubWebsocketMessage{T}"/> with payload type <typeparamref name="T"/> is recieved.</param>
    /// <returns>A new <see cref="ProcessWebsocketMessage"/> with the handler function added.</returns>
    public static ProcessWebsocketMessage Map<T>(this ProcessWebsocketMessage process, Func<EventSubWebsocketMessage<T>, CancellationToken, ValueTask> handleMessage)
        => async (message, ct) =>
        {
            Validation<EventSubWebsocketMessage> result = await process(message, ct);
            return await result.Match<ValueTask<Validation<EventSubWebsocketMessage>>>(
                onError: e => ValueTask.FromResult<Validation<EventSubWebsocketMessage>>(e),
                onValid: async content =>
                {
                    if (content is EventSubWebsocketMessage<T> typedContent)
                        await handleMessage(typedContent, ct);
                    return content;
                });
        };

    /// <summary>
    /// Assign a function that is called when an error occurs during message processing.
    /// </summary>
    /// <param name="process"><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></param>
    /// <param name="handleError"></param>
    /// <returns><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></returns>
    public static ProcessWebsocketMessage MapError(this ProcessWebsocketMessage process, Func<Error, CancellationToken, ValueTask> handleError)
        => async (message, ct) =>
        {
            Validation<EventSubWebsocketMessage> result = await process(message, ct);
            return await result.Match<ValueTask<Validation<EventSubWebsocketMessage>>>(
                onError: async e =>
                {
                    await handleError(e, ct);
                    return e;
                },
                onValid: content => ValueTask.FromResult<Validation<EventSubWebsocketMessage>>(content)
                );
        };

    /// <summary>
    /// Assign a function to call when a notification of type <typeparamref name="T"/> is received for an active EventSub subscription.
    /// </summary>
    /// <typeparam name="T">The notification type to call <paramref name="handleNotification"/> for.</typeparam>
    /// <param name="process"><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></param>
    /// <param name="handleNotification">The function to call when a notification of type <typeparamref name="T"/> is received.</param>
    /// <returns><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></returns>
    public static ProcessWebsocketMessage MapNotification<T>(this ProcessWebsocketMessage process, Func<T, CancellationToken, ValueTask> handleNotification)
        where T : IEventSubNotification
        => process.Map<NotificationMessagePayload>((message, ct) => message.Payload.Value is T notification
                ? handleNotification(notification, ct)
                : ValueTask.CompletedTask
            );

    /// <summary>
    /// Assign a function to call when a subscription is revoked by Twitch.
    /// </summary>
    /// <remarks>
    /// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#revoking-your-subscription">Revoking Your Subscription</see> for more information.
    /// </remarks>
    /// <param name="process"><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></param>
    /// <param name="handleSubscriptionRevoked">The function to call when a subscription is revoked.</param>
    /// <returns><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></returns>
    public static ProcessWebsocketMessage MapSubscriptionRevoked(this ProcessWebsocketMessage process, Func<EventSubSubscription, CancellationToken, ValueTask> handleSubscriptionRevoked)
        => process.Map<RevocationMessagePayload>((message, ct) => handleSubscriptionRevoked(message.Payload.Subscription, ct));

    /// <summary>
    /// Assign a function to call when a welcome message is received from the server.
    /// </summary>
    /// <remarks>
    /// This can be called multiple times throughout the life of the object due to reconnects.
    /// Be sure to update existing EventSub subscriptions with the updated session id.
    /// </remarks>
    /// <param name="process"><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></param>
    /// <param name="handleWelcome">The function to call when a welcome message is received.</param>
    /// /// <returns><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></returns>
    public static ProcessWebsocketMessage MapWelcome(this ProcessWebsocketMessage process, Func<EventSubWebsocketSession, CancellationToken, ValueTask> handleWelcome)
        => process.Map<WelcomeMessagePayload>((message, ct) => handleWelcome(message.Payload.Session, ct));

    /// <summary>
    /// Assign a function to call when a reconnect message is recieved from the server.
    /// </summary>
    /// <remarks>
    /// You can use <see cref="StartEventSubWebsocketClientExtensions.WithReconnects(StartEventSubWebsocketClient, Action{Exception}?)"/>
    /// to enable automatic orchestration of the reconnection handoff at the client level.
    /// </remarks>
    /// <param name="process"><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></param>
    /// <param name="handleReconnect">The function to call when a reconnect message is received.</param>
    /// /// <returns><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></returns>
    public static ProcessWebsocketMessage MapReconnect(this ProcessWebsocketMessage process, Func<EventSubReconnectSession, CancellationToken, ValueTask> handleReconnect)
        => process.Map<ReconnectMessagePayload>((message, ct) => handleReconnect(message.Payload.Session, ct));

    /// <summary>
    /// Assign a function to call when a keepalive message is recieved from the server.
    /// </summary>
    /// <param name="process"><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></param>
    /// <param name="handleKeepalive">The function to call when a keepalive message is received.</param>
    /// <returns><inheritdoc cref="Map{T}(ProcessWebsocketMessage, Func{EventSubWebsocketMessage{T}, CancellationToken, ValueTask})"/></returns>
    public static ProcessWebsocketMessage MapKeepalive(this ProcessWebsocketMessage process, Func<CancellationToken, ValueTask> handleKeepalive)
        => process.Map<KeepaliveMessagePayload>((message, ct) => handleKeepalive(ct));
}
