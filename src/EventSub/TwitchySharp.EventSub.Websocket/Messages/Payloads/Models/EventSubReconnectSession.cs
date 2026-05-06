namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;

/// <summary>
/// Contains information about the current session in the context of a reconnect request from Twitch.
/// </summary>
public record EventSubReconnectSession
{
    /// <summary>
    /// <inheritdoc cref="EventSubWebsocketSession.Id"/>
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// <inheritdoc cref="EventSubWebsocketSession.Status"/>
    /// </summary>
    public required EventSubSessionStatus Status { get; init; }
    /// <summary>
    /// The URL that Twitch is requesting a reconnect to.
    /// </summary>
    public required string ReconnectUrl { get; init; }
    /// <summary>
    /// <inheritdoc cref="EventSubWebsocketSession.ConnectedAt"/>
    /// </summary>
    public required DateTimeOffset ConnectedAt { get; init; }
}
