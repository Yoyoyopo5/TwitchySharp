using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Events.Channel.Chat;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.Chat;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.Chat;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Chat;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatMessage"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatmessage">Channel Chat Message</see> for more information.
/// </remarks>
public record ChannelChatMessageNotification : EventSubNotification<ChannelChatMessageEvent, ChannelChatMessageCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelChatMessage"/>.
/// </summary>
public record ChannelChatMessageCondition : BroadcasterUserCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatMessage"/> event.
/// </summary>
public record ChannelChatMessageEvent : IHaveBroadcaster, IHaveUser, IHaveChannelChatMessage
{
    /// <summary>
    /// The user id of the broadcaster (channel) the message was sent in.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) the message was sent in.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) the message was sent in.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the user who sent the message.
    /// </summary>
    public required string ChatterUserId { get; init; }
    string IHaveUser.UserId => ChatterUserId;
    /// <summary>
    /// The display name of the user who sent the message.
    /// </summary>
    public required string ChatterUserName { get; init; }
    string IHaveUser.UserName => ChatterUserName;
    /// <summary>
    /// The login (username) of the user who sent the message.
    /// </summary>
    public required string ChatterUserLogin { get; init; }
    string IHaveUser.UserLogin => ChatterUserLogin;
    /// <summary>
    /// The id of the message.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The chat message.
    /// </summary>
    public required ChannelChatMessage Message { get; init; }
    /// <summary>
    /// The type of message.
    /// </summary>
    public required ChannelChatMessageType MessageType { get; init; }
    /// <summary>
    /// The badges of the chatter.
    /// </summary>
    public required ChannelChatMessageBadge[] Badges { get; init; }
    /// <summary>
    /// The cheer if the message contains a bits cheer.
    /// </summary>
    public ChannelChatMessageCheer? Cheer { get; init; }
    /// <summary>
    /// The color of the chatter's name in the chat room.
    /// This is a hexadecimal RGB color code in the form <c>#&lt;RGB&gt;</c>. 
    /// This may be empty if the user hasn't picked a name color.
    /// </summary>
    public required string Color { get; init; }
    /// <summary>
    /// The reply if the message is a reply to another message.
    /// </summary>
    public ChannelChatMessageReply? Reply { get; init; }
    /// <summary>
    /// The id of the channel points custom reward that was redeemed if the message included one.
    /// </summary>
    public string? ChannelPointsCustomRewardId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) the message came from if it was sent during a shared chat session from another broadcaster's chat.
    /// </summary>
    public string? SourceBroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) the message came from if it was sent during a shared chat session from another broadcaster's chat.
    /// </summary>
    public string? SourceBroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) the message came from if it was sent during a shared chat session from another broadcaster's chat.
    /// </summary>
    public string? SourceBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the message in the source broadcaster's chat.
    /// Is <see langword="null"/> if the message did not come from another broadcaster during a shared chat session.
    /// </summary>
    public string? SourceMessageId { get; init; }
    /// <summary>
    /// The badges of the chatter in the source broadcaster's chat.
    /// Is <see langword="null"/> if the message did not come from another broadcaster during a shared chat session.
    /// </summary>
    public ChannelChatMessageBadge[]? SourceBadges { get; init; }
}