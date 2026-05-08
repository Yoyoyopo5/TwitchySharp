using TwitchySharp.EventSub.Enums.Events.Automod.Message;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Automod.Message;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Automod.Message;

namespace TwitchySharp.EventSub.Models.Notifications.Automod.Message;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodMessageUpdate"/>
/// </summary>
/// <remarks>
/// <see cref="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodmessageupdate">Automod Message Update</see> for more information.
/// </remarks>
public record AutomodMessageUpdateNotification : EventSubNotification<AutomodMessageUpdateEvent, AutomodMessageUpdateCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.AutomodMessageUpdate"/>
/// </summary>
public record AutomodMessageUpdateCondition : BroadcasterModeratorCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodMessageUpdate"/> event.
/// </summary>
public record AutomodMessageUpdateEvent : IHaveAutomodHeldMessage, IHaveAutomodHeldMessageStatusUpdate, IHaveBroadcaster, IHaveUser, IHaveModerator // Twitch docs are really inconsistent with this event type, may need to revisting in testing.
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
    /// The category that the message was flagged under.
    /// </summary>
    public required AutomodMessageCategory Category { get; init; }
    /// <summary>
    /// The level of severity for the caught message.
    /// Ranges from 1 to 4.
    /// </summary>
    public required int Level { get; init; }
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
    /// The status of the updated automod message.
    /// </summary>
    public required AutomodMessageUpdateStatus Status { get; init; }
}
