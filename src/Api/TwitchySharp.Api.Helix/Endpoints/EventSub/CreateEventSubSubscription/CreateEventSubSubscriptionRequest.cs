using System.Text.Json.Serialization;

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
/// <para>
/// The identity and scopes for the request are automatically determined based on the <see cref="Subscription"/> transport and type.
/// You do not need to manually configure them unless you need to override the default behavior.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-eventsub-subscription">Create EventSub Subscription</see> for more information.
/// </remarks>
public record CreateEventSubSubscriptionRequest
    : TwitchHelixRequest<CreateEventSubSubscriptionResponse>
{
    protected override string Path => "/eventsub/subscriptions";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext
        => Subscription.GetRequestAuthorizationContext();
    public override object? ContentObject => (CreateEventSubSubscriptionRequestData)Subscription;

    /// <summary>
    /// The subscription to create.
    /// </summary>
    public required EventSubSubscriptionSpecification Subscription { get; init; }
}

internal record CreateEventSubSubscriptionRequestData
{
    [JsonPropertyName("type")]
    public required EventSubSubscriptionTypeName Type { get; init; }
    [JsonPropertyName("version")]
    public required EventSubSubscriptionTypeVersion Version { get; init; }
    [JsonPropertyName("condition")]
    public required IReadOnlyDictionary<ConditionKey, object> Condition { get; init; }
    [JsonPropertyName("transport")]
    public required EventSubSubscriptionTransportSpecification Transport { get; init; }
    [JsonPropertyName("is_batching_enabled")]
    public bool? IsBatchingEnabled => Type == EventSubSubscriptionTypeName.DropEntitlementGrant
        ? true : null;

    public static explicit operator CreateEventSubSubscriptionRequestData(EventSubSubscriptionSpecification subscription)
        => new()
        {
            Type = subscription.Type.Type.Type,
            Version = subscription.Type.Type.Version,
            Condition = subscription.Type.Condition.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            Transport = subscription.Transport
        };
}
