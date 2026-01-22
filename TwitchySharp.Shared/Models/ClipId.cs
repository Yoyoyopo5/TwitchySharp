using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch clip.
/// </summary>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<ClipId, string>))]
public readonly record struct ClipId(string Value) : IWrapValue<string>
{
    public static implicit operator string(ClipId id)
        => id.Value;
    public override string ToString()
        => Value;
}