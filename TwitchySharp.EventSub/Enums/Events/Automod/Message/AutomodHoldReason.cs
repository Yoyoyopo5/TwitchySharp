using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Automod.Message;

/// <summary>
/// Contains static definitions for possible reasons Automod holds a message.
/// </summary>
/// <param name="Value"></param>
[Wrapper<string>]
public readonly partial record struct AutomodHoldReason(string Value)
{
    /// <summary>
    /// The Automod held the message due to its configuration.
    /// </summary>
    public static AutomodHoldReason Automod { get; } = new("automod");
    /// <summary>
    /// The Automod held the message due to a manually blocked term appearing in it.
    /// </summary>
    public static AutomodHoldReason BlockedTerm { get; } = new("blocked_term");
}
