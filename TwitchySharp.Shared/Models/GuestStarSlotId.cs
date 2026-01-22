using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Guest Star slot.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<GuestStarSlotId, string>))]
public readonly record struct GuestStarSlotId(string Value) : IWrapValue<string>
{
    public static implicit operator string(GuestStarSlotId id)
        => id.Value;
    public override string ToString()
        => Value;
}