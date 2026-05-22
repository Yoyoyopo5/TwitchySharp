namespace TwitchySharp.EventSub.Websocket.Functional;

/// <summary>
/// An id for a specific Twitch EventSub Websocket message.
/// </summary>
/// <remarks>
/// Twitch sends messages at least once, but if Twitch is unsure of whether you received a notification, it'll resend the message.
/// This means you may receive a notification twice. If Twitch resends the message, the message id will be the same.
/// </remarks>
/// <param name="Value">The string value of the message id.</param>
public readonly partial record struct WebsocketMessageId(string Value);
