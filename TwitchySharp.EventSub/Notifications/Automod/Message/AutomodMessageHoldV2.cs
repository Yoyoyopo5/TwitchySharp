using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Automod.Message;
using TwitchySharp.EventSub.Interfaces.Automod.Message;
using TwitchySharp.EventSub.Models.Automod.Message;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Automod.Message;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodMessageHoldV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodmessagehold-v2">Automod Message Hold V2</see> for more information.
/// </remarks>
public record AutomodMessageHoldV2Notification : EventSubNotification<AutomodMessageHoldV2Event, AutomodMessageHoldV2Condition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.AutomodMessageHoldV2"/>
/// </summary>
public record AutomodMessageHoldV2Condition : BroadcasterModeratorCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodMessageHoldV2"/> event.
/// </summary>
public record AutomodMessageHoldV2Event : IAutomodMessageEvent, IAutomodMessageV2Event
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the Automod caught the message for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the Automod caught the message for.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the Automod caught the message for.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the user that sent the caught message.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the caught message.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that sent the caught message.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The id of the message that was flagged by the Automod.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The message that was flagged.
    /// </summary>
    public required AutomodCaughtChatMessage Message { get; init; }
    /// <summary>
    /// The date and time when the Automod caught the message.
    /// </summary>
    public required DateTimeOffset HeldAt { get; init; }
    /// <summary>
    /// The reason the Automod caught the message.
    /// </summary>
    public required AutomodHoldReason Reason { get; init; }
    /// <summary>
    /// Contains information about the Automod settings that caused the hold.
    /// Is <see langword="null"/> unless <see cref="Reason"/> is <see cref="AutomodHoldReason.Automod"/>.
    /// </summary>
    public AutomodHold? Automod { get; init; }
    /// <summary>
    /// Contains information about the blocked term that caused the hold.
    /// Is <see langword="null"/> unless <see cref="Reason"/> is <see cref="AutomodHoldReason.BlockedTerm"/>.
    /// </summary>
    public BlockedTermHold? BlockedTerm { get; init; }
}
