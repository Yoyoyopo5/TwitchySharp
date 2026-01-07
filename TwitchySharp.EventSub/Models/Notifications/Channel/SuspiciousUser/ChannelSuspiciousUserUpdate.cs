using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Events.Channel.SuspiciousUser;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.SuspiciousUser;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.SuspiciousUser;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSuspiciousUserUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsuspicious_userupdate">Channel Suspicious User Update</see> for more information.
/// </remarks>
public record ChannelSuspiciousUserUpdateNotification : EventSubNotification<ChannelSuspiciousUserUpdateEvent, ChannelSuspiciousUserUpdateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelSuspiciousUserUpdate"/>.
/// </summary>
public record ChannelSuspiciousUserUpdateCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSuspiciousUserUpdate"/> event.
/// </summary>
public record ChannelSuspiciousUserUpdateEvent : IHaveSuspiciousUser, IHaveBroadcaster, IHaveModerator
{
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The user id of the suspicious user.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the suspicious user.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the suspicious user.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The current status of the suspicious user as set by a moderator.
    /// </summary>
    public required SuspiciousUserStatus LowTrustStatus { get; init; }
    /// <summary>
    /// The user id of the moderator that updated the treatment for the suspicious user.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The display name of the moderator that updated the treatment for the suspicious user.
    /// </summary>
    public required string ModeratorUserName { get; init; }
    /// <summary>
    /// The login (username) of the moderator that updated the treatment for the suspicious user.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
}
