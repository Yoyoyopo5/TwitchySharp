using System.Collections.Immutable;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;
/// <summary>
/// <inheritdoc cref="EventSubSubscription"/>
/// </summary>
/// <typeparam name="TCondition">The type of the subscription's condition.</typeparam>
public record EventSubSubscription<TCondition> : EventSubSubscription
    where TCondition : class
{
    /// <summary>
    /// <inheritdoc cref="EventSubSubscription.Condition"/>
    /// </summary>
    public new required TCondition Condition { get; init; }
}

/// <summary>
/// Contains information about the subscription that this notification is for.
/// </summary>
public record EventSubSubscription
{
    /// <summary>
    /// The id of the subscription.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The type of the subscription event data.
    /// </summary>
    public required string Type { get; init; }
    /// <summary>
    /// The version definition of the subscription event data.
    /// </summary>
    public required string Version { get; init; }
    /// <summary>
    /// The status of the subscription.
    /// </summary>
    public required EventSubSubscriptionStatus Status { get; init; }
    /// <summary>
    /// How much the subscription counts against your limit. 
    /// See <see href="https://dev.twitch.tv/docs/eventsub/manage-subscriptions/#subscription-limits">Subscription Limits</see> for more information.
    /// </summary>
    public required int Cost { get; init; }
    /// <summary>
    /// Subscription type specific parameters used to create the subscription.
    /// </summary>
    public ImmutableDictionary<string, object>? Condition { get; init; }
    /// <summary>
    /// The transport of the subscription.
    /// </summary>
    public required EventSubTransport Transport { get; init; }
    /// <summary>
    /// The date and time when this notification was sent.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}
