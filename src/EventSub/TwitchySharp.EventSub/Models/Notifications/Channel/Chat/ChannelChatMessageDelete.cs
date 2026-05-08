using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Chat;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatMessageDelete"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatmessage_delete">Channel Chat Message Delete</see> for more information.
/// </remarks>
public record ChannelChatMessageDeleteNotification : EventSubNotification<ChannelChatMessageDeleteEvent, ChannelChatMessageDeleteCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelChatMessageDelete"/>.
/// </summary>
public record ChannelChatMessageDeleteCondition : BroadcasterUserCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatMessageDelete"/> event.
/// </summary>
public record ChannelChatMessageDeleteEvent : IHaveBroadcaster, IHaveUser
{
    /// <summary>
    /// The user id of the broadcaster (channel) where the message was deleted.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) where the message was deleted.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) where the message was deleted.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the user whose message was deleted.
    /// </summary>
    public required string TargetUserId { get; init; }
    string IHaveUser.UserId => TargetUserId;
    /// <summary>
    /// The display name of the user whose message was deleted.
    /// </summary>
    public required string TargetUserName { get; init; }
    string IHaveUser.UserName => TargetUserName;
    /// <summary>
    /// The login (username) of the user whose message was deleted.
    /// </summary>
    public required string TargetUserLogin { get; init; }
    string IHaveUser.UserLogin => TargetUserLogin;
    /// <summary>
    /// The id of the deleted message.
    /// </summary>
    public required string MessageId { get; init; }
}
