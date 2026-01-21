using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch emote set.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<EmoteId, string>))]
public readonly record struct EmoteSetId(string Value) : IWrapValue<string>
{
    public static implicit operator string(EmoteSetId id)
        => id.Value;
    public override string ToString()
        => Value;
}