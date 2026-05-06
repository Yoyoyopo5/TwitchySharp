using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.Subscription;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Subscription;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSubscriptionGift"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsubscriptiongift">Channel Subscription Gift</see> for more information.
/// </remarks>
public record ChannelSubscriptionGiftNotification : EventSubNotification<ChannelSubscriptionGiftEvent, ChannelSubscriptionGiftCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelSubscriptionGift"/>.
/// </summary>
public record ChannelSubscriptionGiftCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSubscriptionGift"/> event.
/// </summary>
public record ChannelSubscriptionGiftEvent : IHaveSubscription, IHaveBroadcaster
{
    /// <summary>
    /// The id of the user that sent the subscription gift.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the subscription gift.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that sent the subscription gift.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the subscription was made to.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the subscription was made to.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the subscription was made to.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The number of subscriptions in the subscription gift.
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The tier of the subscription gift.
    /// </summary>
    public required SubscriptionTier Tier { get; init; }
    /// <summary>
    /// The total number of subscriptions gifted by the user in this channel.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public int? CumulativeTotal { get; init; }
    /// <summary>
    /// Indicates whether the subscription was gifted anonymously.
    /// </summary>
    public required bool IsAnonymous { get; init; }
}
