using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Extensions.Enums;

/// <summary>
/// Contains static definitions for cost types for Twitch extension transactions.
/// </summary>
/// <param name="Value">The string value of the cost type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ExtensionProductCostType, string>))]
public record ExtensionProductCostType(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static ExtensionProductCostType Bits { get; } = new("bits");
}
