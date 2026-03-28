using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch extension.
/// </summary>
/// <remarks>
/// This also functions as a <see cref="ClientId"/> and can be used to authenticate requests.
/// </remarks>
/// <param name="Value">The string value of the extension id.</param>
[JsonConverter(typeof(WrapperJsonConverter<ExtensionId, string>))]
public readonly record struct ExtensionId(string Value) : IWrapValue<string>
{
    public static implicit operator ClientId(ExtensionId id)
        => new(id.Value);
    public static implicit operator string(ExtensionId id)
        => id.Value;
    public override string ToString()
        => Value;
}