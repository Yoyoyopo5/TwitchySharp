using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Enums;

/// <summary>
/// Contains static definitions for possible conduit transport methods.
/// </summary>
/// <param name="Value">The string value of the transport method.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ConduitTransportMethod, string>))]
public record ConduitTransportMethod(string Value) : ValueBackedEnum<string>(Value)
{
    public static ConduitTransportMethod Websocket { get; } = new("websocket");
    public static ConduitTransportMethod Webhook { get; } = new("webhook");
}
