using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch EventSub subscription.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<EventSubSubscriptionId, string>))]
public readonly record struct EventSubSubscriptionId(string Value) : IWrapValue<string>
{
    public static implicit operator string(EventSubSubscriptionId id)
        => id.Value;
    public override string ToString()
        => Value;
}