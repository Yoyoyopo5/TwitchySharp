namespace TwitchySharp.EventSub.Websocket.Clients;

/// <summary>
/// Stops and disposes the Websocket client.
/// </summary>
/// <returns>
/// A <see cref="Task"/> that completes when the client is fully stopped and disposed.
/// </returns>
public delegate Task StopWebsocketClient(CancellationToken ct = default);
/// <summary>
/// Starts the Websocket client.
/// </summary>
/// <param name="ct">Cancellation Token</param>
/// <returns>A <see cref="Task"/> that completes with a function that stops the Websocket client.</returns>
public delegate Task<StopWebsocketClient> StartWebsocketClient(CancellationToken ct = default);
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
}

/// <summary>
/// Contains a function for creating a <see cref="StartEventSubWebsocketClient"/> function from an arbitrary Websocket client.
/// </summary>
public static class EventSubWebsocketClient
{
    /// <summary>
    /// Wrap an arbitrary Websocket client implementation into a <see cref="StartEventSubWebsocketClient"/> function.
    /// </summary>
    /// <param name="createClient">The client creation function.</param>
    /// <returns>
    /// A function that starts the Websocket client.
    /// </returns>
    public static StartEventSubWebsocketClient Create(CreateWebsocketClient createClient)
        => async (pipeline, url, ct)
        => await url.ToUri()
            .Map(uri => createClient(
                new EventSubWebsocketClientContext
                {
                    Uri = uri,
                    OnMessage = async (stream, messageCt) => await pipeline(new(stream), messageCt)
                }
                ))
            .Match(
                onError: e => throw new ArgumentException("The url could not be converted to a Uri.", nameof(url)),
                onValid: startClient => startClient(ct)
                );
}
