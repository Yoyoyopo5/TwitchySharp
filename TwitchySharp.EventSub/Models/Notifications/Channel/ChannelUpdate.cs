using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelupdate">Channel Update</see> for more information.
/// </remarks>
public record ChannelUpdateNotification : EventSubNotification<ChannelUpdateEvent, ChannelUpdateCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelUpdate"/>.
/// </summary>
public record ChannelUpdateCondition : BroadcasterCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelUpdate"/> event.
/// </summary>
public record ChannelUpdateEvent : IHaveBroadcaster
{
    /// <summary>
    /// The user id of the broadcaster (channel) that changed their channel information.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that changed their channel information.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that changed their channel information.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The updated title of the broadcaster's channel.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The updated language of the broadcaster's channel.
    /// </summary>
    public required string Language { get; init; }
    /// <summary>
    /// The updated id of the category (game) of the broadcaster's channel.
    /// </summary>
    public required string CategoryId { get; init; }
    /// <summary>
    /// The updated name of the category (game) of the broadcaster's channel.
    /// </summary>
    public required string CategoryName { get; init; }
    /// <summary>
    /// The updated content classification labels currently applied to the broadcaster's channel.
    /// </summary>
    public required ContentClassificationLabelId[] ContentClassificationLabels { get; init; }
}
