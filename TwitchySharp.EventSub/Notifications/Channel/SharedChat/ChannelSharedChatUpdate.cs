using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Notifications.Channel.SharedChat;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSharedChatSessionUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshared_chatupdate">Channel Shared Chat Update</see> for more information.
/// </remarks>
public record ChannelSharedChatUpdateNotification : EventSubNotification<ChannelSharedChatUpdateEvent, ChannelSharedChatUpdateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelSharedChatSessionUpdate"/>.
/// </summary>
public record ChannelSharedChatUpdateCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Shared Chat Update notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSharedChatSessionUpdate"/> event.
/// </summary>
public record ChannelSharedChatUpdateEvent
{
    /// <summary>
    /// The id of the shared chat session.
    /// </summary>
    public required string SessionId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) from the subscription condition that is active in the shared chat session.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) from the subscription condition that is active in the shared chat session.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) from the subscription condition that is active in the shared chat session.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that is hosting the shared chat session.
    /// </summary>
    public required string HostBroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that is hosting the shared chat session.
    /// </summary>
    public required string HostBroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that is hosting the shared chat session.
    /// </summary>
    public required string HostBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The list of broadcasters participating in the shared chat session.
    /// </summary>
    public required SharedChatParticipant[] Paritipicants { get; init; }
}
