using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSubscribe"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsubscribe">Channel Subscribe</see> for more information.
/// </remarks>
public record ChannelSubscribeNotification : EventSubNotification<ChannelSubscribeEvent, ChannelSubscribeCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelSubscribe"/>.
/// </summary>
public record ChannelSubscribeCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Subscribe notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSubscribe"/> event.
/// </summary>
public record ChannelSubscribeEvent
{
    /// <summary>
    /// The id of the user that subscribed.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that subscribed.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that subscribed.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the subscription was made to.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the subscription was made to.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the subscription was made to.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The tier of the subscription.
    /// </summary>
    public required SubscriptionTier Tier { get; init; }
    /// <summary>
    /// Indicates whether the subscription is a gift.
    /// </summary>
    public required bool IsGift { get; init; }
}
