using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Notifications.Channel.Shoutout;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ShoutoutReceived"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshoutoutreceive">Shoutout Received</see> for more information.
/// </remarks>
public record ShoutoutReceivedNotification : EventSubNotification<ShoutoutReceivedEvent, ShoutoutReceivedCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ShoutoutReceived"/>.
/// </summary>
public record ShoutoutReceivedCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ShoutoutReceived"/> event.
/// </summary>
public record ShoutoutReceivedEvent : ShoutoutEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required string FromBroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required string FromBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required string FromBroadcasterUserName { get; init; }
}
