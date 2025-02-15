using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.EventSub.Enums;

/// <summary>
/// Possible EventSub transport methods.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<EventSubTransportMethod, string>))]
public record EventSubTransportMethod(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static EventSubTransportMethod Webhook { get; } = new("webhook");
    public static EventSubTransportMethod Websocket { get; } = new("websocket");
    public static EventSubTransportMethod Conduit { get; } = new("conduit");
}
