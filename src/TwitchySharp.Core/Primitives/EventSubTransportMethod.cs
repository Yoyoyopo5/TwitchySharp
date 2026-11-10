using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// Possible EventSub transport methods.
/// </summary>
[Wrapper<string>]
public readonly partial record struct EventSubTransportMethod(string Value)
{
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/">webhook</see> transport.
    /// </summary>
    public static EventSubTransportMethod Webhook { get; } = new("webhook");
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/eventsub/handling-websocket-events/">WebSocket</see> transport.
    /// </summary>
    public static EventSubTransportMethod Websocket { get; } = new("websocket");
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/eventsub/handling-conduit-events/">conduit</see> transport.
    /// </summary>
    public static EventSubTransportMethod Conduit { get; } = new("conduit");
}
