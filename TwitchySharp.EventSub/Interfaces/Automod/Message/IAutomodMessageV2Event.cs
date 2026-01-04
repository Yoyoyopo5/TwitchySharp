using TwitchySharp.EventSub.Enums.Automod.Message;
using TwitchySharp.EventSub.Models.Automod.Message;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Interfaces.Automod.Message;

/// <summary>
/// The interface for Automod message V2 events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.AutomodMessageHoldV2"/>,
/// <see cref="EventSubSubscriptionType.AutomodMessageUpdateV2"/>.
/// </remarks>
public interface IAutomodMessageV2Event
{
    AutomodHoldReason Reason { get; init; }
    /// <summary>
    /// Contains information about the Automod settings that caused the hold.
    /// Is <see langword="null"/> unless <see cref="Reason"/> is <see cref="AutomodHoldReason.Automod"/>.
    /// </summary>
    AutomodHold? Automod { get; init; }
    /// <summary>
    /// Contains information about the blocked term that caused the hold.
    /// Is <see langword="null"/> unless <see cref="Reason"/> is <see cref="AutomodHoldReason.BlockedTerm"/>.
    /// </summary>
    BlockedTermHold? BlockedTerm { get; init; }
}
