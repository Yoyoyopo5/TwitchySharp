using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An id representing a specific Twitch EventSub subscription type version.
/// </summary>
/// <param name="Value">The string value of the version.</param>
[Wrapper<string>]
public readonly partial record struct EventSubSubscriptionTypeVersion(string Value)
{
    public static EventSubSubscriptionTypeVersion Version1 { get; } = new("1");
    public static EventSubSubscriptionTypeVersion Version2 { get; } = new("2");
    public static EventSubSubscriptionTypeVersion Beta { get; } = new("beta");
}

