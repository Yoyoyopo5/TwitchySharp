using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains static definitions for types of extension configuration segments.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ExtensionConfigurationSegmentType, string>))]
public record ExtensionConfigurationSegmentType(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// Configuration segment is delivered to views of your extension on the associated channel and
    /// can be set by developers and broadcaster.
    /// </summary>
    public static ExtensionConfigurationSegmentType Broadcaster { get; } = new("broadcaster");
    /// <summary>
    /// Configuration segment is delivered to views of your extension on the associated channel and
    /// can be set by developers.
    /// </summary>
    public static ExtensionConfigurationSegmentType Developer { get; } = new("developer");
    /// <summary>
    /// Configuration segment is delivered to every view of your extension, regardless of the channel and
    /// can be set by developers.
    /// </summary>
    public static ExtensionConfigurationSegmentType Global { get; } = new("global");
}
