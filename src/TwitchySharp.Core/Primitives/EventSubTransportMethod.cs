using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// Possible EventSub transport methods.
/// </summary>
[Wrapper<string>]
public readonly partial record struct EventSubTransportMethod(string Value)
{
    public static EventSubTransportMethod Webhook { get; } = new("webhook");
    public static EventSubTransportMethod Websocket { get; } = new("websocket");
    public static EventSubTransportMethod Conduit { get; } = new("conduit");
}
