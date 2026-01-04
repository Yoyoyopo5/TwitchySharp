using TwitchySharp.EventSub.Enums.Automod.Message;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Interfaces.Automod.Message;

/// <summary>
/// The interface for Automod message V1 events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.AutomodMessageHold"/>,
/// <see cref="EventSubSubscriptionType.AutomodMessageUpdate"/>.
/// </remarks>
public interface IAutomodMessageV1Event
{
    AutomodMessageCategory Category { get; init; }
    /// <summary>
    /// The level of severity for the caught message.
    /// Ranges from 1 to 4.
    /// </summary>
    int Level { get; init; }
}
