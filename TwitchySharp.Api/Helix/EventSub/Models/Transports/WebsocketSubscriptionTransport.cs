namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// An EventSub transport that uses <see href="https://dev.twitch.tv/docs/eventsub/handling-websocket-events/">WebSockets</see>.
/// </summary>
/// <param name="SessionId">
/// The session id of the WebSocket connection to send notifications to.
/// When you connect to EventSub using WebSockets, the server returns this id in the Welcome message.
/// </param>
public sealed record WebsocketSubscriptionTransport(string SessionId)
    : NewEventSubSubscriptionTransport
{
    /// <summary>
    /// The transport method identifier.
    /// </summary>
    public override string Method => "websocket";
    /// <summary>
    /// The id of the WebSocket session that notifications are sent to.
    /// </summary>
    public override string SessionId { get; } = SessionId;
}
