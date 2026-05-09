using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Contains information about an existing EventSub subscription.
/// </summary>
public record EventSubSubscription
{
    /// <summary>
    /// The id of the subscription.
    /// </summary>
    public required EventSubSubscriptionId Id { get; init; }
    /// <summary>
    /// The subscription's status.
    /// </summary>
    /// <remarks>
    /// Note that the subscriber receives events only for <see cref="EventSubSubscriptionStatus.Enabled"/> subscriptions.
    /// </remarks>
    public required EventSubSubscriptionStatus Status { get; init; }
    /// <summary>
    /// The subscription’s type name.
    /// </summary>
    /// <remarks>
    /// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#subscription-types">Subscription Types</see>.
    /// </remarks>
    public required EventSubSubscriptionTypeName Type { get; init; }
    /// <summary>
    /// The version number that identifies this definition of the subscription type's data.
    /// </summary>
    /// <remarks>
    /// This in addition to the <see cref="Type"/> property identify exactly what notification will be sent through this subscription.
    /// </remarks>
    public required EventSubSubscriptionTypeVersion Version { get; init; }
    /// <summary>
    /// The subscription’s parameter values.
    /// </summary>
    /// <remarks>
    /// The exact keys depend on what the subscription type expects.
    /// </remarks>
    public required ImmutableDictionary<ConditionKey, string> Condition { get; init; }
    /// <summary>
    /// The date and time when the subscription was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The transport details used to send the notifications.
    /// </summary>
    public required EventSubSubscriptionTransport Transport { get; init; }
    /// <summary>
    /// The amount that the subscription counts against the application's limit.
    /// </summary>
    /// <remarks>
    /// See <see href="https://dev.twitch.tv/docs/eventsub/manage-subscriptions/#subscription-limits">subscription limits</see>.
    /// </remarks>
    public required int Cost { get; init; }
}

internal static class EventSubSubscriptionExtensions
{
    /// <summary>
    /// Get a <see cref="EventSubSubscriptionType"/> based on the type name and version of the subscription.
    /// </summary>
    /// <param name="subscription">The subscription to get the subscription type of.</param>
    /// <returns>The <see cref="EventSubSubscriptionType"/> of the subscription.</returns>
    public static EventSubSubscriptionType GetSubscriptionType(this EventSubSubscription subscription)
        => new(subscription.Type, subscription.Version);
}
