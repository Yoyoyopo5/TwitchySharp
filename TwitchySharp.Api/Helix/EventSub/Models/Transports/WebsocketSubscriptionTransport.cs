using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// An EventSub transport that uses <see href="https://dev.twitch.tv/docs/eventsub/handling-websocket-events/">WebSockets</see>.
/// </summary>
public sealed record WebsocketSubscriptionTransport
    : NewEventSubSubscriptionTransport
{
    /// <param name="sessionId">
    /// The session id of the WebSocket connection to send notifications to.
    /// When you connect to EventSub using WebSockets, the server returns this id in the Welcome message.
    /// </param>
    public WebsocketSubscriptionTransport(EventSubWebsocketSessionId sessionId)
        => (Method, SessionId) = (EventSubTransportMethod.Websocket, sessionId);
}
