using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.Subscription;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Subscription;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSubscriptionEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsubscriptionend">Channel Subscription End</see> for more information.
/// </remarks>
public record ChannelSubscriptionEndNotification : EventSubNotification<ChannelSubscriptionEndEvent, ChannelSubscriptionEndCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelSubscriptionEnd"/>.
/// </summary>
public record ChannelSubscriptionEndCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSubscriptionEnd"/> event.
/// </summary>
public record ChannelSubscriptionEndEvent : IHaveSubscription, IHaveBroadcaster, IHaveUser
{
    /// <summary>
    /// The id of the user whose subscription ended.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user whose subscription ended.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user whose subscription ended.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the original subscription was made to.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the original subscription was made to.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the original subscription was made to.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The tier of the subscription that ended.
    /// </summary>
    public required SubscriptionTier Tier { get; init; }
    /// <summary>
    /// Indicates whether the subscription was a gift.
    /// </summary>
    public required bool IsGift { get; init; }
}
