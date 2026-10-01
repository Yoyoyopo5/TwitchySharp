namespace TwitchySharp.EventSub;
/// <summary>
/// <inheritdoc cref="IEventSubSubscription"/>
/// </summary>
/// <typeparam name="TCondition">The type of the subscription's condition.</typeparam>
public record EventSubSubscription<TCondition> : IEventSubSubscription
{
    public required EventSubSubscriptionId Id { get; init; }
    public required EventSubSubscriptionTypeName Type { get; init; }
    public required EventSubSubscriptionTypeVersion Version { get; init; }
    public required EventSubSubscriptionStatus Status { get; init; }
    public required int Cost { get; init; }
    public required EventSubTransport Transport { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required TCondition Condition { get; init; }
    object? IEventSubSubscription.Condition => Condition;
}

/// <summary>
/// Contains information about the subscription that this notification is for.
/// </summary>
public interface IEventSubSubscription
{
    /// <summary>
    /// The id of the subscription.
    /// </summary>
    EventSubSubscriptionId Id { get; }
    /// <summary>
    /// The type of the subscription event data.
    /// </summary>
    EventSubSubscriptionTypeName Type { get; }
    /// <summary>
    /// The version definition of the subscription event data.
    /// </summary>
    EventSubSubscriptionTypeVersion Version { get; }
    /// <summary>
    /// The status of the subscription.
    /// </summary>
    EventSubSubscriptionStatus Status { get; }
    /// <summary>
    /// How much the subscription counts against your limit. 
    /// See <see href="https://dev.twitch.tv/docs/eventsub/manage-subscriptions/#subscription-limits">Subscription Limits</see> for more information.
    /// </summary>
    int Cost { get; }
    /// <summary>
    /// The transport of the subscription.
    /// </summary>
    EventSubTransport Transport { get; }
    /// <summary>
    /// The date and time when this notification was sent.
    /// </summary>
    DateTimeOffset CreatedAt { get; }
    /// <summary>
    /// Subscription type specific parameters used to create the subscription.
    /// </summary>
    object? Condition { get; }
}
