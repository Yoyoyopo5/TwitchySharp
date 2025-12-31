using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelCheer"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcheer">Channel Cheer</see> for more information.
/// </remarks>
public record ChannelCheerNotification : EventSubNotification<ChannelCheerEvent, ChannelCheerCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelCheer"/>.
/// </summary>
public record ChannelCheerCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Cheer notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelCheer"/> event.
/// </summary>
public record ChannelCheerEvent
{
    /// <summary>
    /// Indicates whether the cheer was made anonymously.
    /// </summary>
    public required bool IsAnonymous { get; init; }
    /// <summary>
    /// The id of the user that cheered.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that cheered.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that cheered.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that received the cheer.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that received the cheer.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that received the cheer.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The message that was sent with the cheer.
    /// </summary>
    public required string Message { get; init; }
    /// <summary>
    /// The number of Bits cheered.
    /// </summary>
    public required int Bits { get; init; }
}
