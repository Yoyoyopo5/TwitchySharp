using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch extension.
/// </summary>
/// <param name="Value">The string value of the extension id.</param>
[JsonConverter(typeof(WrapperJsonConverter<ExtensionId, string>))]
public readonly record struct ExtensionId(string Value) : IWrapValue<string>
{
    public static implicit operator string(ExtensionId id)
        => id.Value;
    public override string ToString()
        => Value;
}