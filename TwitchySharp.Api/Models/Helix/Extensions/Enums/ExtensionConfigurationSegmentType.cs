using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Extensions.Enums;

/// <summary>
/// Contains static definitions for types of extension configuration segments.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ExtensionConfigurationSegmentType, string>))]
public record ExtensionConfigurationSegmentType(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static ExtensionConfigurationSegmentType Broadcaster { get; } = new("broadcaster");
    public static ExtensionConfigurationSegmentType Developer { get; } = new("developer");
    public static ExtensionConfigurationSegmentType Global { get; } = new("global");
}
