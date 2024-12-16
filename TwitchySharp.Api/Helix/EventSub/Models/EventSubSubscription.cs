using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Contains information about an existing EventSub subscription.
/// </summary>
public record EventSubSubscription
{
    /// <summary>
    /// The id of the subscription.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The subscription's status.
    /// Note that the subscriber receives events only for <see cref="EventSubSubscriptionStatus.Enabled"/> subscriptions.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<EventSubSubscriptionStatus>))]
    public required EventSubSubscriptionStatus Status { get; init; }
    /// <summary>
    /// The subscription’s type name.
    /// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#subscription-types">Subscription Types</see>.
    /// </summary>
    public required string Type { get; init; }
    /// <summary>
    /// The version number that identifies this definition of the subscription type's data.
    /// This in addition to the <see cref="Type"/> property identify exactly what notification will be sent through this subscription.
    /// </summary>
    public required string Version { get; init; }
    /// <summary>
    /// The subscription’s parameter values.
    /// The exact keys depend on what the subscription type expects.
    /// </summary>
    public required ImmutableDictionary<string, string> Conditon { get; init; }
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
    /// See <see href="https://dev.twitch.tv/docs/eventsub/manage-subscriptions/#subscription-limits">subscription limits</see>.
    /// </summary>
    public required int Cost { get; init; }
    /// <summary>
    /// The total number of subscriptions the application has created.
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The sum of all your <see href="https://dev.twitch.tv/docs/eventsub/manage-subscriptions/#subscription-limits">subscription costs</see>.
    /// </summary>
    public required int TotalCost { get; init; }
    /// <summary>
    /// The maximum total cost that you’re allowed to incur for all subscriptions you create.
    /// </summary>
    public required int MaxTotalCost { get; init; }
}
