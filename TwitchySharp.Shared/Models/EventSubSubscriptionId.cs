using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch EventSub subscription.
/// </summary>
/// <param name="Value">The string value of the id.</param>
public readonly partial record struct EventSubSubscriptionId(string Value) : IWrapValue<string>;