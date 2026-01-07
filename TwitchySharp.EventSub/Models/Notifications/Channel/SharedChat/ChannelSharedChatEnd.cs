using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.SharedChat;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.SharedChat;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.SharedChat;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSharedChatSessionEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshared_chatend">Channel Shared Chat End</see> for more information.
/// </remarks>
public record ChannelSharedChatEndNotification : EventSubNotification<ChannelSharedChatEndEvent, ChannelSharedChatEndCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelSharedChatSessionEnd"/>.
/// </summary>
public record ChannelSharedChatEndCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSharedChatSessionEnd"/> event.
/// </summary>
public record ChannelSharedChatEndEvent : IHaveSharedChat, IHaveBroadcaster
{
    public required string SessionId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) from the subscription condition that is no longer active in the shared chat session.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) from the subscription condition that is no longer active in the shared chat session.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) from the subscription condition that is no longer active in the shared chat session.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    public required string HostBroadcasterUserId { get; init; }
    public required string HostBroadcasterUserName { get; init; }
    public required string HostBroadcasterUserLogin { get; init; }
    /// <summary>
    /// Unsupported for this event type, defaults to empty array.
    /// </summary>
    SharedChatParticipant[] IHaveSharedChat.Participant => [];
}
