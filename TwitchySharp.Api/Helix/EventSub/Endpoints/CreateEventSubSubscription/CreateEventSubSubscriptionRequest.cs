using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Constants;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Creates an EventSub subscription.
/// </summary>
/// <remarks>
/// <para>
/// If you use the <see cref="WebsocketSubscriptionTransport"/>, an access token must be a user access token with the required <see cref="Scope"/> for the subscription type.
/// </para>
/// <para>
/// If you use the <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, the access token must be an app access token.
/// If the subscription type requires a user access token authorization, the user must have created a user access token with the required <see cref="Scope"/> for this application (i.e., for this application's client id).
/// It is not required to send this user access token in the request.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-eventsub-subscription">Create EventSub Subscription</see> for more information.
/// </remarks>
public record CreateEventSubSubscriptionRequest
    : TwitchHelixRequest<CreateEventSubSubscriptionResponse>
{
    protected override string Path => "/eventsub/subscriptions";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
    public override object? ContentObject => (CreateEventSubSubscriptionRequestData)Subscription;

    /// <summary>
    /// The subscription to create.
    /// </summary>
    public required NewEventSubSubscription Subscription { get; set; }
}

internal record CreateEventSubSubscriptionRequestData
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }
    [JsonPropertyName("version")]
    public required string Version { get; init; }
    [JsonPropertyName("condition")]
    public required IReadOnlyDictionary<string, object> Condition { get; init; }
    [JsonPropertyName("transport")]
    public required NewEventSubSubscriptionTransport Transport { get; init; }
    [JsonPropertyName("is_batching_enabled")]
    public bool? IsBatchingEnabled => Type switch // Kind of jank but this is the only type that requires this.
    {
        EventSubSubscriptionTypeNames.DROP_ENTITLEMENT_GRANT => true,
        _ => null
    };

    public static explicit operator CreateEventSubSubscriptionRequestData(NewEventSubSubscription subscription)
        => new()
        {
            Type = subscription.Type.Type.Type,
            Version = subscription.Type.Type.Version,
            Condition = subscription.Type.Condition.ToDictionary(kvp => (string)kvp.Key, kvp => kvp.Value),
            Transport = subscription.Transport
        };
}
