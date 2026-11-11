using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// An id representing a specific Twitch EventSub subscription type version.
/// </summary>
/// <param name="Value">The string value of the version.</param>
[Wrapper<string>]
public readonly partial record struct EventSubSubscriptionTypeVersion(string Value)
{
    /// <summary>
    /// Version 1.
    /// </summary>
    public static EventSubSubscriptionTypeVersion Version1 { get; } = new("1");
    /// <summary>
    /// Version 2.
    /// </summary>
    public static EventSubSubscriptionTypeVersion Version2 { get; } = new("2");
    /// <summary>
    /// Beta version.
    /// </summary>
    public static EventSubSubscriptionTypeVersion Beta { get; } = new("beta");
}

