using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.GuestStar;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelGuestStarSessionBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelguest_star_sessionbegin">Channel Guest Star Session Begin</see> for more information.
/// </remarks>
public record ChannelGuestStarSessionBeginNotification : EventSubNotification<ChannelGuestStarSessionBeginEvent, ChannelGuestStarSessionBeginCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelGuestStarSessionBegin"/>.
/// </summary>
public record ChannelGuestStarSessionBeginCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelGuestStarSessionBegin"/> event.
/// </summary>
public record ChannelGuestStarSessionBeginEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    // Docs also allude to a moderator here in the example, but it's not in the spec.
    /// <summary>
    /// The id of the Guest Star session that was started.
    /// </summary>
    public required string SessionId { get; init; }
    /// <summary>
    /// The date and time when the Guest Star session began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
