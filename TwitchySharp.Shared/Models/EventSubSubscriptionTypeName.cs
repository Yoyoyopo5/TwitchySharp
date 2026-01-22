using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch EventSub subscription type name.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<EventSubSubscriptionTypeName, string>))]
public readonly record struct EventSubSubscriptionTypeName(string Value) : IWrapValue<string>
{
    public static implicit operator string(EventSubSubscriptionTypeName id)
        => id.Value;
    public override string ToString()
        => Value;
}