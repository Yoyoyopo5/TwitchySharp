using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch EventSub subscription type name.
/// </summary>
/// <param name="Value">The string value of the id.</param>
public readonly partial record struct EventSubSubscriptionTypeName(string Value) : IWrapValue<string>;