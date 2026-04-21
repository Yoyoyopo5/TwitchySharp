using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains static definitions for possible suspicious chatter types.
/// </summary>
/// <param name="Value">The string value of the suspicious chatter type.</param>
[Wrapper<string>]
public readonly partial record struct SuspiciousUserType(string Value)
{
    public static SuspiciousUserType ManuallyAdded { get; } = new("MANUALLY_ADDED");
    public static SuspiciousUserType DetectedBanEvader { get; } = new("DETECTED_BAN_EVADER");
    public static SuspiciousUserType DetectedSuspiciousChatter { get; } = new("DETECTED_SUS_CHATTER"); // Lol, no idea what this one even means.
    public static SuspiciousUserType BannedInSharedChannel { get; } = new("BANNED_IN_SHARED_CHANNEL");
}
