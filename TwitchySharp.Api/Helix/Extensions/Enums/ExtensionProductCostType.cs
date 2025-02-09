using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Cost types for Twitch extension transactions.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ExtensionProductCostType, string>))]
public record ExtensionProductCostType(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static ExtensionProductCostType Bits { get; } = new("bits");
}
