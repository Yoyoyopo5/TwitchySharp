using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Enums.Events.Channel.SuspiciousUser;

/// <summary>
/// Contains static definitions for possible suspicious user types.
/// </summary>
/// <param name="Value">The string value of the suspicious user type.</param>
[Wrapper<string>]
public readonly partial record struct ChannelSuspiciousUserType(string Value)
{
    /// <summary>
    /// The suspicious user was manually tagged by a moderator.
    /// </summary>
    public static ChannelSuspiciousUserType ManuallyAdded { get; } = new("manually_added");
    /// <summary>
    /// The suspicious user was marked by Twitch as a potential ban evader.
    /// </summary>
    public static ChannelSuspiciousUserType BanEvader { get; } = new("ban_evader");
    /// <summary>
    /// The suspicious user was banned in a channel sharing bans with the broadcaster.
    /// </summary>
    public static ChannelSuspiciousUserType BannedInSharedChannel { get; } = new("banned_in_shared_channel");
}
