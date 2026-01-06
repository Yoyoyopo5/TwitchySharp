using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Notifications.Channel.ShieldMode;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ShieldModeEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshield_modeend">Shield Mode End</see> for more information.
/// </remarks>
public record ShieldModeEndNotification : EventSubNotification<ShieldModeEndEvent, ShieldModeEndCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ShieldModeEnd"/>.
/// </summary>
public record ShieldModeEndCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ShieldModeEnd"/> event.
/// </summary>
public record ShieldModeEndEvent : IHaveBroadcaster, IHaveModerator
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
    /// The date and time when Shield Mode was disabled.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
}
