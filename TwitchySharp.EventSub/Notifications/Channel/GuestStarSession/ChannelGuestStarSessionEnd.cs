using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelGuestStarSessionEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelguest_star_sessionend">Channel Guest Star Session End</see> for more information.
/// </remarks>
public record ChannelGuestStarSessionEndNotification : EventSubNotification<ChannelGuestStarSessionEndEvent, ChannelGuestStarSessionEndCondition>
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelGuestStarSessionEnd"/>.
/// </summary>
public record ChannelGuestStarSessionEndCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Guest Star Session End notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The user id of the broadcaster or a moderator in the broadcaster's chat to get notifications on behalf of.
    /// </summary>
    public required string ModeratorUserId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelGuestStarSessionEnd"/> event.
/// </summary>
public record ChannelGuestStarSessionEndEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that was in the ended Guest Star session who this subscription is associated with.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that was in the ended Guest Star session who this subscription is associated with..
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that was in the ended Guest Star session who this subscription is associated with..
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the Guest Star session that was ended.
    /// </summary>
    public required string SessionId { get; init; }
    /// <summary>
    /// The date and time when the Guest Star session began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The date and time when the Guest Star session ended.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
    /// <summary>
    /// The user id of the broadcaster who started the Guest Star session that ended.
    /// </summary>
    public required string HostUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster who started the Guest Star session that ended.
    /// </summary>
    public required string HostUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster who started the Guest Star session that ended.
    /// </summary>
    public required string HostUserLogin { get; init; }
}
