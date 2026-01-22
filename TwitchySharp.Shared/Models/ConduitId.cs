using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch conduit transport.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<ConduitId, string>))]
public readonly record struct ConduitId(string Value) : IWrapValue<string>
{
    public static implicit operator string(ConduitId id)
        => id.Value;
    public override string ToString()
        => Value;
}