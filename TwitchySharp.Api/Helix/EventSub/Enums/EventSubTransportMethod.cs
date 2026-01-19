using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Contains static definitions for possible EventSub transport methods.
/// </summary>
/// <param name="Value">The string value of the transport method.</param>
public record EventSubTransportMethod(string Value) : ValueBackedEnum<string>(Value)
{
    public EventSubTransportMethod Webhook { get; } = new("webhook");
    public EventSubTransportMethod Websocket { get; } = new("websocket");
    public EventSubTransportMethod Conduit { get; } = new("conduit");
}
