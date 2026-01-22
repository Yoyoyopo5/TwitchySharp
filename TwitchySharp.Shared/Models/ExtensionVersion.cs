using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// A Twitch extension version.
/// </summary>
/// <param name="Value">The string value of the extension version.</param>
[JsonConverter(typeof(WrapperJsonConverter<ExtensionVersion, string>))]
public readonly record struct ExtensionVersion(string Value) : IWrapValue<string>
{
    public static implicit operator string(ExtensionVersion version)
        => version.Value;
    public override string ToString()
        => Value;
}