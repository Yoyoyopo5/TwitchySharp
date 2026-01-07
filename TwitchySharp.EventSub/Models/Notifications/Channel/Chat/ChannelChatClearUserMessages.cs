using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Chat;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatClearUserMessages"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatclear_user_messages">Channel Chat Clear User Messages</see> for more information.
/// </remarks>
public record ChannelChatClearUserMessagesNotification : EventSubNotification<ChannelChatClearUserMessagesEvent, ChannelChatClearUserMessagesCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelChatClearUserMessages"/>.
/// </summary>
public record ChannelChatClearUserMessagesCondition : BroadcasterUserCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatClearUserMessages"/> event.
/// </summary>
public record ChannelChatClearUserMessagesEvent : IHaveBroadcaster, IHaveUser
{
    /// <summary>
    /// The user id of the broadcaster (channel) where the user's chat messages were cleared.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) where the user's chat messages were cleared.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) where the user's chat messages were cleared.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the user whose chat messages were cleared.
    /// </summary>
    public required string TargetUserId { get; init; }
    string IHaveUser.UserId => TargetUserId;
    /// <summary>
    /// The display name of the user whose chat messages were cleared.
    /// </summary>
    public required string TargetUserName { get; init; }
    string IHaveUser.UserName => TargetUserName;
    /// <summary>
    /// The login (username) of the user whose chat messages were cleared.
    /// </summary>
    public required string TargetUserLogin { get; init; }
    string IHaveUser.UserLogin => TargetUserLogin;
}
