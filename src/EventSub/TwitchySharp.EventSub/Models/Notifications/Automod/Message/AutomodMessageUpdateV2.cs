using TwitchySharp.EventSub.Enums.Events.Automod.Message;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Automod.Message;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Automod.Message;

namespace TwitchySharp.EventSub.Models.Notifications.Automod.Message;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodMessageUpdateV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodmessageupdate-v2">Automod Message Update V2</see> for more information.
/// </remarks>
public record AutomodMessageUpdateV2Notification : EventSubNotification<AutomodMessageUpdateV2Event, AutomodMessageUpdateV2Condition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.AutomodMessageUpdateV2"/>
/// </summary>
public record AutomodMessageUpdateV2Condition : BroadcasterModeratorCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodMessageUpdateV2"/> event.
/// </summary>
public record AutomodMessageUpdateV2Event : IHaveAutomodHeldMessageV2, IHaveAutomodHeldMessageStatusUpdate, IHaveBroadcaster, IHaveUser, IHaveModerator
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
    /// <summary>
    /// The user id of the moderator that updated the held Automod message.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The display name of the moderator that updated the held Automod message.
    /// </summary>
    public required string ModeratorUserName { get; init; }
    /// <summary>
    /// The login (username) of the moderator that updated the held Automod message.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The status of the held message after the update.
    /// </summary>
    public required AutomodMessageUpdateStatus Status { get; init; }
}
