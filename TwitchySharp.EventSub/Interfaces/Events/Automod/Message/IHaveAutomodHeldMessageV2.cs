using TwitchySharp.EventSub.Enums.Events.Automod.Message;
using TwitchySharp.EventSub.Models.Events.Automod.Message;

namespace TwitchySharp.EventSub.Interfaces.Events.Automod.Message;

/// <summary>
/// An Automod held message.
/// </summary>
public interface IHaveAutomodHeldMessageV2
{
    /// <summary>
    /// The id of the message that was flagged by the Automod.
    /// </summary>
    string MessageId { get; }
    /// <summary>
    /// The message that was flagged.
    /// </summary>
    AutomodCaughtChatMessage Message { get; }
    /// <summary>
    /// The date and time when the Automod caught the message.
    /// </summary>
    DateTimeOffset HeldAt { get; }
    /// <summary>
    /// The reason the Automod caught the message.
    /// </summary>
    AutomodHoldReason Reason { get; }
    /// <summary>
    /// Contains information about the Automod settings that caused the hold.
    /// Is <see langword="null"/> unless <see cref="Reason"/> is <see cref="AutomodHoldReason.Automod"/>.
    /// </summary>
    AutomodHold? Automod { get; }
    /// <summary>
    /// Contains information about the blocked term that caused the hold.
    /// Is <see langword="null"/> unless <see cref="Reason"/> is <see cref="AutomodHoldReason.BlockedTerm"/>.
    /// </summary>
    BlockedTermHold? BlockedTerm { get; }
}
