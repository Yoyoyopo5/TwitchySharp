using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch EventSub subscription type version.
/// </summary>
/// <param name="Value">The string value of the version.</param>
[JsonConverter(typeof(WrapperJsonConverter<EventSubSubscriptionTypeName, string>))]
public readonly record struct EventSubSubscriptionTypeVersion(string Value) : IWrapValue<string>
{
    public static implicit operator string(EventSubSubscriptionTypeVersion id)
        => id.Value;
    public override string ToString()
        => Value;
}

