
namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

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
