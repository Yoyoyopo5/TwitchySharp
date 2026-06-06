namespace TwitchySharp.EventSub.Websocket.Clients;

/// <summary>
/// Stops and disposes the Websocket client.
/// </summary>
/// <remarks>
/// Note that this function is not awaited by the default <see cref="EventSubWebsocketClient"/> upon cancellation.
/// </remarks>
/// <returns>
/// A <see cref="Task"/> that completes when the client is disposed.
/// </returns>
public delegate Task StopWebsocketClient();
/// <summary>
/// Starts the Websocket client.
/// </summary>
/// <param name="ct">Cancellation Token</param>
/// <returns>A <see cref="Task"/> that completes with a function that stops the Websocket client.</returns>
public delegate Task<StopWebsocketClient> StartWebsocketClient(CancellationToken ct);
/// <summary>
/// Creates and configures the Websocket client.
/// </summary>
/// <param name="ctx">The client context for use in configuration.</param>
/// <returns>A function that starts the created Websocket client.</returns>
public delegate StartWebsocketClient CreateWebsocketClient(EventSubWebsocketClientContext ctx);

/// <summary>
/// An EventSub Websocket client context.
/// </summary>
/// <remarks>
/// Use this to configure a Websocket client.
/// </remarks>
public record EventSubWebsocketClientContext
{
    /// <summary>
    /// The Twitch EventSub Websocket url to connect to.
    /// </summary>
    public required Uri Uri { get; init; }
    /// <summary>
    /// The function that should be called when a message is received.
    /// </summary>
    public required Func<Stream, CancellationToken, ValueTask> OnMessage { get; init; }
    /// <summary>
    /// The function that should be called when a fatal error occurs and the connection must be closed.
    /// </summary>
    public required Action<Exception> OnError { get; init; }
}

/// <summary>
/// Contains a function for creating a <see cref="ListenToEventSubWebsocketClient"/> function from an arbitrary Websocket client.
/// </summary>
public static class EventSubWebsocketClient
{
    /// <summary>
    /// Wrap an arbitrary Websocket client into a <see cref="ListenToEventSubWebsocketClient"/> function.
    /// </summary>
    /// <param name="createClient">The client creation function.</param>
    /// <returns>
    /// A function returning a long-lived <see cref="Task"/> that completes when the cancellation token is cancelled (or an exception is thrown).
    /// </returns>
    public static ListenToEventSubWebsocketClient Create(CreateWebsocketClient createClient)
        => async (pipeline, url, ct) =>
        {
            TaskCompletionSource tcs = new();

            StopWebsocketClient stopClient = await url.ToUri()
                .Map(uri => createClient(
                    new EventSubWebsocketClientContext
                    {
                        Uri = uri,
                        OnMessage = async (stream, messageCt) => await pipeline(new(stream), messageCt),
                        OnError = error => tcs.TrySetException(error)
                    }
                    ))
                .Match(
                    onError: e => throw new ArgumentException("The url could not be converted to a Uri.", nameof(url)),
                    onValid: startClient => startClient(ct)
                    );

            await using CancellationTokenRegistration cancellation = ct.Register(() =>
            {
                tcs.TrySetResult();
                _ = stopClient();
            });

            await tcs.Task;
        };
}
