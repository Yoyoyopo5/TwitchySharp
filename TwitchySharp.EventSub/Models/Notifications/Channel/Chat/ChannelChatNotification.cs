using TwitchySharp.EventSub.Enums.Events.Channel.Chat;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.Chat;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.Chat;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Chat;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatNotification"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatnotification">Channel Chat Notification</see> for more information.
/// </remarks>
public record ChannelChatNotificationNotification : EventSubNotification<ChannelChatNotificationEvent, ChannelChatNotificationCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelChatNotification"/>.
/// </summary>
public record ChannelChatNotificationCondition : BroadcasterUserCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatNotification"/> event.
/// </summary>
public record ChannelChatNotificationEvent : IHaveBroadcaster, IHaveUser, IHaveChannelChatMessage
{
    /// <summary>
    /// The user id of the broadcaster (channel) where the chat notification occurred.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) where the chat notification occurred.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) where the chat notification occurred.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The user id of the chatter that triggered the chat notification.
    /// </summary>
    public required string ChatterUserId { get; init; }
    string IHaveUser.UserId => ChatterUserId;
    /// <summary>
    /// The login (username) of the chatter that triggered the chat notification.
    /// </summary>
    public required string ChatterUserLogin { get; init; }
    string IHaveUser.UserLogin => ChatterUserLogin;
    /// <summary>
    /// The display name of the chatter that triggered the chat notification.
    /// </summary>
    public required string ChatterUserName { get; init; }
    string IHaveUser.UserName => ChatterUserName;
    /// <summary>
    /// Indicates whether the chatter that triggered the chat notification is anonymous.
    /// </summary>
    public required bool ChatterIsAnonymous { get; init; }
    /// <summary>
    /// The color of the user's name in chat.
    /// </summary>
    public required string Color { get; init; } // Not sure if this is actually required, need to test with unset user color. Pretty sure it is set to empty string.
    /// <summary>
    /// The set of chat badges the chatter has in the channel.
    /// </summary>
    public required ChannelChatMessageBadge[] Badges { get; init; } // Twitch docs have a typo for this.
    /// <summary>
    /// The message that Twitch shows in the chat room for this notification.
    /// </summary>
    public required string SystemMessage { get; init; }
    /// <summary>
    /// The id of the message that triggered the chat notification.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The message that triggered the chat notification.
    /// </summary>
    public required ChannelChatMessage Message { get; init; }
    /// <summary>
    /// The type of notification. This determines which of the other properties are populated.
    /// </summary>
    public required ChannelChatNotificationType NoticeType { get; init; }
    // So this is kind of fucked, but it's how Twitch decided to design it.
    /// <summary>
    /// Information about the subscription event, 
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.Subscription"/>.
    /// </summary>
    public ChannelChatNotificationSubscription? Sub { get; init; }
    /// <summary>
    /// Information about the resubscription event,
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.Resubscription"/>.
    /// </summary>
    public ChannelChatNotificationResubscription? Resub { get; init; }
    /// <summary>
    /// Information about the gifted subscription event,
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.GiftedSubscription"/>.
    /// </summary>
    public ChannelChatNotificationGiftedSubscription? SubGift { get; init; }
    /// <summary>
    /// Information about the community subscription gift event,
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.CommunityGiftedSubscription"/>.
    /// </summary>
    public ChannelChatMessageNotificationCommunitySubcriptionGift? CommunitySubGift { get; init; }
    /// <summary>
    /// Information about the gifted subscription paid upgrade event,
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.GiftedSubscriptionPaidUpgrade"/>.
    /// </summary>
    public ChannelChatNotificationGiftedSubscriptionPaidUpgrade? GiftPaidUpgrade { get; init; }
    /// <summary>
    /// Information about the prime subscription paid upgrade event,
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.PrimeSubscriptionPaidUpgrade"/>.
    /// </summary>
    public ChannelChatNotificationPrimeSubscriptionPaidUpgrade? PrimePaidUpgrade { get; init; }
    /// <summary>
    /// Information about the pay it forward event,
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.PayItForward"/>.
    /// </summary>
    public ChannelChatNotificationPayItForward? PayItForward { get; init; }
    /// <summary>
    /// Information about the raid event,
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.Raid"/>.
    /// </summary>
    public ChannelChatNotificationRaid? Raid { get; init; }
    /// <summary>
    /// Information about the unraid event (currently an empty payload),
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.Unraid"/>.
    /// </summary>
    public ChannelChatNotificationUnraid? Unraid { get; init; }
    /// <summary>
    /// Information about the announcement event,
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.Announcement"/>.
    /// </summary>
    public ChannelChatNotificationAnnouncement? Announcement { get; init; }
    /// <summary>
    /// Information about the bits badge tier upgrade event,
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.BitsBadgeTier"/>.
    /// </summary>
    public ChannelChatNotificationBitsBadgeTier? BitsBadgeTier { get; init; }
    /// <summary>
    /// Information about the charity donation event,
    /// if <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.CharityDonation"/>.
    /// </summary>
    public ChannelChatMessageNotificationCharityDonation? CharityDonation { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) the message that triggered the notification was sent from.
    /// This is <see langword="null"/> when a shared chat session is not active, or the message is sent in the same channel as the <see cref="BroadcasterUserId"/>.
    /// </summary>
    public string? SourceBroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) the message that triggered the notification was sent from.
    /// This is <see langword="null"/> when a shared chat session is not active, or the message is sent in the same channel as the <see cref="BroadcasterUserId"/>.
    /// </summary>
    public string? SourceBroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) the message that triggered the notification was sent from.
    /// This is <see langword="null"/> when a shared chat session is not active, or the message is sent in the same channel as the <see cref="BroadcasterUserId"/>.
    /// </summary>
    public string? SourceBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the message that triggered the notification in the source channel.
    /// This is <see langword="null"/> when a shared chat session is not active, or the message is sent in the same channel as the <see cref="BroadcasterUserId"/>.
    /// </summary>
    public string? SourceMessageId { get; init; }
    /// <summary>
    /// The list of chat badges the chatter that sent the message that triggered the notification has in the source channel.
    /// This is <see langword="null"/> when a shared chat session is not active, or the message is sent in the same channel as the <see cref="BroadcasterUserId"/>.
    /// </summary>
    public ChannelChatMessageBadge[]? SourceBadges { get; init; }
    /// <summary>
    /// Information about the subscription event in a shared chat,
    /// if the <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.SharedChatSubscription"/>
    /// and the subscription occurred in a shared chat session to a broadcaster other than <see cref="BroadcasterUserId"/>.
    /// </summary>
    public ChannelChatNotificationSubscription? SharedChatSub { get; init; }
    /// <summary>
    /// Information about the resubscription event in a shared chat,
    /// if the <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.SharedChatResubscription"/>
    /// and the resubscription occurred in a shared chat session to a broadcaster other than <see cref="BroadcasterUserId"/>.
    /// </summary>
    public ChannelChatNotificationResubscription? SharedChatResub { get; init; }
    /// <summary>
    /// Information about the gifted subscription event in a shared chat,
    /// if the <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.SharedChatGiftedSubscription"/>
    /// and the gifted subscription occurred in a shared chat session to a broadcaster other <see cref="BroadcasterUserId"/>.
    /// </summary>
    public ChannelChatNotificationGiftedSubscription? SharedChatSubGift { get; init; }
    /// <summary>
    /// Information about the community gifted subscription event in a shared chat,
    /// if the <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.SharedChatCommunityGiftedSubscription"/>
    /// and the community gifted subscription occurred in a shared chat session to a broadcaster other than <see cref="BroadcasterUserId"/>.
    /// </summary>
    public ChannelChatMessageNotificationCommunitySubcriptionGift? SharedChatCommunitySubGift { get; init; }
    /// <summary>
    /// Information about the gifted subscription paid upgrade event in a shared chat,
    /// if the <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.SharedChatGiftedSubscriptionPaidUpgrade"/>
    /// and the gifted subscription paid upgrade occurred in a shared chat session to a broadcaster other than <see cref="BroadcasterUserId"/>.
    /// </summary>
    public ChannelChatNotificationGiftedSubscriptionPaidUpgrade? SharedChatGiftPaidUpgrade { get; init; }
    /// <summary>
    /// Information about the prime subscription paid upgrade event in a shared chat,
    /// if the <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.SharedChatPrimeSubscriptionPaidUpgrade"/>
    /// and the prime subscription paid upgrade occurred in a shared chat session to a broadcaster other than <see cref="BroadcasterUserId"/>.
    /// </summary>
    public ChannelChatNotificationPrimeSubscriptionPaidUpgrade? SharedChatPrimePaidUpgrade { get; init; }
    /// <summary>
    /// Information about the pay it forward event in a shared chat,
    /// if the <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.SharedChatPayItForward"/>
    /// and the pay it forward event occurred in a shared chat session to a broadcaster other than <see cref="BroadcasterUserId"/>.
    /// </summary>
    public ChannelChatNotificationPayItForward? SharedChatPayItForward { get; init; }
    /// <summary>
    /// Information about the raid event in a shared chat,
    /// if the <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.SharedChatRaid"/>
    /// and the raid event occurred in a shared chat session to a broadcaster other than <see cref="BroadcasterUserId"/>.
    /// </summary>
    public ChannelChatNotificationRaid? SharedChatRaid { get; init; }
    /// <summary>
    /// Information about the announcement event in a shared chat,
    /// if the <see cref="NoticeType"/> is <see cref="ChannelChatNotificationType.SharedChatAnnouncement"/>
    /// and the announcement event occurred in a shared chat session to a broadcaster other than <see cref="BroadcasterUserId"/>.
    /// </summary>
    public ChannelChatNotificationAnnouncement? SharedChatAnnouncement { get; init; }
}