using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// Contains static definitions for possible conduit transport methods.
/// </summary>
/// <param name="Value">The string value of the transport method.</param>
[Wrapper<string>]
public readonly partial record struct ConduitTransportMethod(string Value)
{
    /// <inheritdoc cref="EventSubTransportMethod.Websocket"/>
    public static ConduitTransportMethod Websocket { get; } = new("websocket");
    /// <inheritdoc cref="EventSubTransportMethod.Webhook"/>
    public static ConduitTransportMethod Webhook { get; } = new("webhook");
}
