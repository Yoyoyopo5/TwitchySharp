using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.EventSub.Notifications.Channel;

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
public record ChannelChatNotificationCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Channel Chat Notification notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The id of the user to read chat as.
    /// </summary>
    public required string UserId { get; init; }
}

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatNotification"/> event.
/// </summary>
public record ChannelChatNotificationEvent
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
    /// <summary>
    /// The display name of the chatter that triggered the chat notification.
    /// </summary>
    public required string ChatterUserName { get; init; }
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

/// <summary>
/// Contains static definitions for chat notification types.
/// </summary>
/// <param name="Value">The string value of the notification type.</param>
public record ChannelChatNotificationType(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// A user subscribed to the channel.
    /// </summary>
    public static ChannelChatNotificationType Subscription { get; } = new("sub");
    /// <summary>
    /// A user resubscribed to the channel.
    /// </summary>
    public static ChannelChatNotificationType Resubscription { get; } = new("resub");
    /// <summary>
    /// A user was gifted a subscription to the channel.
    /// </summary>
    public static ChannelChatNotificationType GiftedSubscription { get; } = new("sub_gift");
    /// <summary>
    /// A community gifted subscription occurred in the channel.
    /// </summary>
    /// <remarks>
    /// Dev Note: I'm not entirely sure what this one is, but I'm guessing it's when a user gifts multiple subscriptions at once to random viewers in the channel.
    /// </remarks>
    public static ChannelChatNotificationType CommunityGiftedSubscription { get; } = new("community_sub_gift");
    /// <summary>
    /// A user that received a gifted subscription upgraded to a paid subscription.
    /// </summary>
    public static ChannelChatNotificationType GiftedSubscriptionPaidUpgrade { get; } = new("gift_paid_upgrade");
    /// <summary>
    /// A user that had an active Prime subscription upgraded to a paid subscription.
    /// </summary>
    public static ChannelChatNotificationType PrimeSubscriptionPaidUpgrade { get; } = new("prime_paid_upgrade");
    /// <summary>
    /// A raid was started.
    /// </summary>
    public static ChannelChatNotificationType Raid { get; } = new("raid"); // Need to verify this documentation.
    /// <summary>
    /// A raid was cancelled.
    /// </summary>
    public static ChannelChatNotificationType Unraid { get; } = new("unraid");
    /// <summary>
    /// A user that received a gifted subscription decided to "pay it forward" by gifting a subscription to another user.
    /// </summary>
    public static ChannelChatNotificationType PayItForward { get; } = new("pay_it_forward");
    /// <summary>
    /// A chat announcement was made in the channel.
    /// </summary>
    public static ChannelChatNotificationType Announcement { get; } = new("announcement");
    /// <summary>
    /// A user received a bits badge tier upgrade.
    /// </summary>
    /// <remarks>
    /// Dev Note: I'm pretty sure this happens when a user cheers a certain amount of total bits in a channel.
    /// </remarks>
    public static ChannelChatNotificationType BitsBadgeTier { get; } = new("bits_badge_tier");
    /// <summary>
    /// A charity donation was made.
    /// </summary>
    public static ChannelChatNotificationType CharityDonation { get; } = new("charity_donation");
    /// <summary>
    /// A user subscribed to a channel in a shared chat.
    /// </summary>
    public static ChannelChatNotificationType SharedChatSubscription { get; } = new("shared_chat_sub");
    /// <summary>
    /// A user resubscribed to a channel in a shared chat.
    /// </summary>
    public static ChannelChatNotificationType SharedChatResubscription { get; } = new("shared_chat_resub");
    /// <summary>
    /// A user was gifted a subscription to a channel in a shared chat.
    /// </summary>
    public static ChannelChatNotificationType SharedChatGiftedSubscription { get; } = new("shared_chat_sub_gift");
    /// <summary>
    /// A community gifted subscription occurred in a shared chat.
    /// </summary>
    public static ChannelChatNotificationType SharedChatCommunityGiftedSubscription { get; } = new("shared_chat_community_sub_gift");
    /// <summary>
    /// A user that received a gifted subscription in a shared chat upgraded to a paid subscription.
    /// </summary>
    public static ChannelChatNotificationType SharedChatGiftedSubscriptionPaidUpgrade { get; } = new("shared_chat_gift_paid_upgrade");
    /// <summary>
    /// A user that had an active Prime subscription in a shared chat upgraded to a paid subscription.
    /// </summary>
    public static ChannelChatNotificationType SharedChatPrimeSubscriptionPaidUpgrade { get; } = new("shared_chat_prime_paid_upgrade");
    /// <summary>
    /// A raid was started in a shared chat.
    /// </summary>
    public static ChannelChatNotificationType SharedChatRaid { get; } = new("shared_chat_raid");
    /// <summary>
    /// A user that received a gifted sub decided to "pay it forward" by gifting a subscription to another user in a shared chat.
    /// </summary>
    public static ChannelChatNotificationType SharedChatPayItForward { get; } = new("shared_chat_pay_it_forward");
    /// <summary>
    /// A chat announcement was made in a shared chat.
    /// </summary>
    public static ChannelChatNotificationType SharedChatAnnouncement { get; } = new("shared_chat_announcement");
}

/// <summary>
/// Contains information about a channel subscription that appeared in a chat notification.
/// </summary>
public record ChannelChatNotificationSubscription
{
    /// <summary>
    /// The tier of the subscription.
    /// </summary>
    public required SubscriptionTier SubTier { get; init; }
    /// <summary>
    /// Indicates if the subscription was obtained through Amazon Prime.
    /// </summary>
    public required bool IsPrime { get; init; }
    /// <summary>
    /// The number of months the subscription is for.
    /// </summary>
    public required int DurationMonths { get; init; }
}

/// <summary>
/// Contains information about a channel resubscription that appeared in a chat notification.
/// </summary>
public record ChannelChatNotificationResubscription
{
    /// <summary>
    /// The total number of months the user has been subscribed to the channel.
    /// </summary>
    public required int CumulativeMonths { get; init; }
    /// <summary>
    /// The number of months the resubscription is for.
    /// </summary>
    public required int DurationMonths { get; init; }
    /// <summary>
    /// The number of consecutive months the user has been subscribed to the channel.
    /// </summary>
    public required int StreakMonths { get; init; }
    /// <summary>
    /// The tier of the subscription.
    /// </summary>
    public required SubscriptionTier SubTier { get; init; }
    /// <summary>
    /// Indicates if the subscription was obtained through Amazon Prime.
    /// </summary>
    public bool? IsPrime { get; init; } // Marked optional in documentation, no idea why. Docs also fucked here, so we'll have to figure it out live.
    /// <summary>
    /// Indicates if the resubscription is the result of a gift.
    /// </summary>
    public bool IsGift { get; init; }
    /// <summary>
    /// Indicates if the resubscription gifter is anonymous.
    /// Is <see langword="null"/> if <see cref="IsGift"/> is <see langword="false"/>.
    /// </summary>
    public bool? GifterIsAnonymous { get; init; }
    /// <summary>
    /// The id of the user that gifted the subscription.
    /// Is <see langword="null"/> if <see cref="IsGift"/> is <see langword="false"/>, or if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserId { get; init; }
    /// <summary>
    /// The display name of the user that gifted the subscription.
    /// Is <see langword="null"/> if <see cref="IsGift"/> is <see langword="false"/>, or if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserName { get; init; }
    /// <summary>
    /// The login (username) of the user that gifted the subscription.
    /// Is <see langword="null"/> if <see cref="IsGift"/> is <see langword="false"/>, or if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserLogin { get; init; }
}

/// <summary>
/// Contains information about a gifted subscription that appeared in a chat notification.
/// </summary>
public record ChannelChatNotificationGiftedSubscription
{
    /// <summary>
    /// The number of months the subscription is for.
    /// </summary>
    public required int DurationMonths { get; init; }
    /// <summary>
    /// The total amount of gifted subscriptions the gifter has given in the channel.
    /// This is <see langword="null"/> if the gifter is anonymous.
    /// </summary>
    public int? CumulativeTotal { get; init; }
    /// <summary>
    /// The id of the user that received the gifted subscription.
    /// </summary>
    public required string RecipientUserId { get; init; }
    /// <summary>
    /// The display name of the user that received the gifted subscription.
    /// </summary>
    public required string RecipientUserName { get; init; }
    /// <summary>
    /// The login (username) of the user that received the gifted subscription.
    /// </summary>
    public required string RecipientUserLogin { get; init; }
    /// <summary>
    /// The tier of the gifted subscription.
    /// </summary>
    public required SubscriptionTier SubTier { get; init; }
    /// <summary>
    /// The id of the associated community gift event.
    /// This is <see langword="null"/> if the gifted subscription is not part of a community gift.
    /// </summary>
    public string? CommunityGiftId { get; init; }
}

/// <summary>
/// Contains information about a specific community subscription gift that appeared in a chat notification.
/// </summary>
public record ChannelChatMessageNotificationCommunitySubcriptionGift
{
    /// <summary>
    /// The id of the community gift event.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The number of subscriptions being gifted.
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The tier of the gifted subscriptions.
    /// </summary>
    public required SubscriptionTier SubTier { get; init; }
    /// <summary>
    /// The cumulative total number of subscriptions the gifter has gifted in the channel.
    /// This is <see langword="null"/> if the gifter is anonymous.
    /// </summary>
    public int? CumulativeTotal { get; init; }
}

/// <summary>
/// Contains information about a gifted subscription paid upgrade that appeared in a chat notification.
/// </summary>
public record ChannelChatNotificationGiftedSubscriptionPaidUpgrade
{
    /// <summary>
    /// Indicates whether the gifter is anonymous.
    /// </summary>
    public required bool GifterIsAnonymous { get; init; }
    /// <summary>
    /// The id of the user that gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserId { get; init; }
    /// <summary>
    /// The display name of the user that gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserName { get; init; }
}

/// <summary>
/// Contains information about a prime subscription paid upgrade that appeared in a chat notification.
/// </summary>
public record ChannelChatNotificationPrimeSubscriptionPaidUpgrade
{
    /// <summary>
    /// The tier of the subscription.
    /// </summary>
    public required SubscriptionTier SubTier { get; init; }
}

/// <summary>
/// Contains information about a "pay it forward" chat notification.
/// </summary>
public record ChannelChatNotificationPayItForward
{
    /// <summary>
    /// Indicates whether the gifter is anonymous.
    /// </summary>
    public required bool GifterIsAnonymous { get; init; }
    /// <summary>
    /// The id of the user who gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserId { get; init; }
    /// <summary>
    /// The display name of the user who gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserName { get; init; }
    /// <summary>
    /// The login (username) of the user who gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserLogin { get; init; }
}

/// <summary>
/// Contains information about a raid chat notification.
/// </summary>
/// <remarks>
/// Dev Note: I'm not sure if this is for an incoming raid or outgoing one, but docs word it as outgoing.
/// </remarks>
public record ChannelChatNotificationRaid
{
    /// <summary>
    /// The id of the user raiding the channel.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the user raiding the channel.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the user raiding the channel.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The number of viewers in the raid.
    /// </summary>
    public required int ViewerCount { get; init; }
    /// <summary>
    /// The profile image URL of the user raiding the channel.
    /// </summary>
    public required string ProfileImageUrl { get; init; }
}

/// <summary>
/// An empty object.
/// </summary>
public record ChannelChatNotificationUnraid // Really need to figure out what this represents
{

}

/// <summary>
/// Contains information about a chat announcement notification.
/// </summary>
public record ChannelChatNotificationAnnouncement
{
    /// <summary>
    /// The color of the announcement.
    /// </summary>
    public required string Color { get; init; } // Might be optional, need to test.
}

/// <summary>
/// Contains information about a bits badge tier upgrade notification.
/// </summary>
public record ChannelChatNotificationBitsBadgeTier
{
    /// <summary>
    /// The tier of the Bits badge (how many Bits are required to acheive it).
    /// For example, <c>100</c>, <c>1000</c>, <c>10000</c>, etc.
    /// </summary>
    public required int Tier { get; init; }
}

/// <summary>
/// Contains information about a charity donation notification.
/// </summary>
public record ChannelChatMessageNotificationCharityDonation
{
    /// <summary>
    /// The name of the charity that was donated to.
    /// </summary>
    public required string CharityName { get; init; }
    /// <summary>
    /// The amount that was donated.
    /// </summary>
    public required CharityAmount Amount { get; init; }
}