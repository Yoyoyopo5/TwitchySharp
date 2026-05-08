using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An id representing a specific Twitch EventSub subscription type version.
/// </summary>
/// <param name="Value">The string value of the version.</param>
[Wrapper<string>]
public readonly partial record struct EventSubSubscriptionTypeVersion(string Value);

