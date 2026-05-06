using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Enums.Events.Channel.Chat;

/// <summary>
/// Contains static definitions for chat notification types.
/// </summary>
/// <param name="Value">The string value of the notification type.</param>
[Wrapper<string>]
public readonly partial record struct ChannelChatNotificationType(string Value)
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
