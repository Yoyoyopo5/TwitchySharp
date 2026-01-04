using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.Chat;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.Chat;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Chat;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatUserMessageHold"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatuser_message_hold">Channel Chat User Message Hold</see> for more information.
/// </remarks>
public record ChannelChatUserMessageHoldNotification : EventSubNotification<ChannelChatUserMessageHoldEvent, ChannelChatUserMessageHoldCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelChatUserMessageHold"/>.
/// </summary>
public record ChannelChatUserMessageHoldCondition : BroadcasterUserCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatUserMessageHold"/> event.
/// </summary>
public record ChannelChatUserMessageHoldEvent : IHaveBroadcaster, IHaveUser, IHaveChannelChatMessage
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the message was sent in.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the message was sent in.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the message was sent in.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that sent the held message.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the held message.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that sent the held message.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The id of the message that was held.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The message that was held.
    /// </summary>
    public required ChannelChatMessage Message { get; init; }
}
