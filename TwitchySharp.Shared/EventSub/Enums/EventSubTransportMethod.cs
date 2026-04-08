using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.EventSub.Enums;

/// <summary>
/// Possible EventSub transport methods.
/// </summary>
public readonly partial record struct EventSubTransportMethod(string Value) : IWrapValue<string>
{
    public static EventSubTransportMethod Webhook { get; } = new("webhook");
    public static EventSubTransportMethod Websocket { get; } = new("websocket");
    public static EventSubTransportMethod Conduit { get; } = new("conduit");
}
