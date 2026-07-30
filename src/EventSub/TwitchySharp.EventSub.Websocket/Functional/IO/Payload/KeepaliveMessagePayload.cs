namespace TwitchySharp.EventSub.Websocket.Functional;
/// <summary>
/// A keepalive message payload. Empty object.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/handling-websocket-events#keepalive-message">Keepalive Message</see> for more information.
/// </remarks>
public readonly record struct KeepaliveMessagePayload;
