using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains static definitions for possible extension configuration locations.
/// </summary>
/// <param name="Value">The string value of the extension configuration location.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ExtensionConfigurationLocation, string>))]
public record ExtensionConfigurationLocation(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The Extensions Configuration Service hosts the configuration.
    /// </summary>
    public static ExtensionConfigurationLocation Hosted { get; } = new("hosted");
    /// <summary>
    /// The Extension Backend Service (EBS) hosts the configuration.
    /// </summary>
    public static ExtensionConfigurationLocation Custom { get; } = new("custom");
    /// <summary>
    /// The extension doesn't require configuration.
    /// </summary>
    public static ExtensionConfigurationLocation None { get; } = new("none");
}
