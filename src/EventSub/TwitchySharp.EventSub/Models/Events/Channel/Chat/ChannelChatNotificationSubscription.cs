
namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

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
