using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.ShieldMode;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ShieldModeBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshield_modebegin">Shield Mode Begin</see> for more information.
/// </remarks>
public record ShieldModeBeginNotification : EventSubNotification<ShieldModeBeginEvent, ShieldModeBeginCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ShieldModeBegin"/>.
/// </summary>
public record ShieldModeBeginCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ShieldModeBegin"/> event.
/// </summary>
public record ShieldModeBeginEvent : IHaveBroadcaster, IHaveModerator
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose Shield Mode status was changed.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose Shield Mode status was changed.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose Shield Mode status was changed.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator who changed the Shield Mode status.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator who changed the Shield Mode status.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator who changed the Shield Mode status.
    /// </summary>
    public required string ModeratorUserName { get; init; }
    /// <summary>
    /// The date and time when Shield Mode was enabled.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
