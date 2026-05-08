using TwitchySharp.EventSub.Enums.Events.Automod.Message;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Automod.Message;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Automod.Message;

namespace TwitchySharp.EventSub.Models.Notifications.Automod.Message;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodMessageHold"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodmessagehold">Automod Message Hold</see> for more information.
/// </remarks>
public record AutomodMessageHoldNotification : EventSubNotification<AutomodMessageHoldEvent, AutomodMessageHoldCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.AutomodMessageHold"/>.
/// </summary>
public record AutomodMessageHoldCondition : BroadcasterModeratorCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodMessageHold"/> event.
/// </summary>
public record AutomodMessageHoldEvent : IHaveAutomodHeldMessage, IHaveBroadcaster, IHaveUser
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
}
