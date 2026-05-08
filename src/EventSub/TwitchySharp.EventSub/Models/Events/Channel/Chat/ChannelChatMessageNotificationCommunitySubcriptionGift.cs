
namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

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
