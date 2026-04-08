using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Enums;

/// <summary>
/// Contains static definitions for possible conduit transport methods.
/// </summary>
/// <param name="Value">The string value of the transport method.</param>
public readonly partial record struct ConduitTransportMethod(string Value) : IWrapValue<string>
{
    public static ConduitTransportMethod Websocket { get; } = new("websocket");
    public static ConduitTransportMethod Webhook { get; } = new("webhook");
}
