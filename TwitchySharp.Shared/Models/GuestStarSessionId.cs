using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Guest Star session.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<GuestStarSessionId, string>))]
public readonly record struct GuestStarSessionId(string Value) : IWrapValue<string>
{
    public static implicit operator string(GuestStarSessionId id)
        => id.Value;
    public override string ToString()
        => Value;
}