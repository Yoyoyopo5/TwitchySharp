using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Constants;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Creates an EventSub subscription.
/// </summary>
/// <remarks>
/// Requires an app (for webhooks, conduit) or user access token (for websocket) with scopes depending on the subscription type.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-eventsub-subscription">Create EventSub Subscription</see> for more information.
/// </remarks>
public record CreateEventSubSubscriptionRequest
    : TwitchHelixRequest<CreateEventSubSubscriptionResponse>
{
    /// <remarks>
    /// <para>
    /// If you use the <see cref="WebsocketSubscriptionTransport"/>, the <paramref name="accessToken"/> must be a user access token with the required <see cref="Scope"/> for the subscription type.
    /// </para>
    /// <para>
    /// If you use the <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, the <paramref name="accessToken"/> must be an app access token. 
    /// If the subscription type requires a user access token authorization, the user must have created a user access token with the required <see cref="Scope"/> for this application (i.e., for this application's client id). 
    /// It is not required to send this user access token in the request.
    /// </para>
    /// </remarks>
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">
    /// If using <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, an app access token.
    /// If using <see cref="WebsocketSubscriptionTransport"/>, a user access token with the subscription type's required <see cref="Scope"/>.
    /// </param>
    /// <param name="subscription">The subscription to create.</param>
    public CreateEventSubSubscriptionRequest(
        ClientId clientId,
        AccessToken accessToken,
        NewEventSubSubscription subscription
        )
        : base(
            "/eventsub/subscriptions",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Post;
        ContentObject = (CreateEventSubSubscriptionRequestData)subscription;
    }
}

internal record CreateEventSubSubscriptionRequestData
{
    public required EventSubSubscriptionTypeName Type { get; init; }
    public required EventSubSubscriptionTypeVersion Version { get; init; }
    public required IReadOnlyDictionary<string, object> Condition { get; init; }
    public required NewEventSubSubscriptionTransport Transport { get; init; }
    public bool? IsBatchingEnabled => Type.Value switch // Kind of jank but this is the only type that requires this.
    {
        EventSubSubscriptionTypeNames.DROP_ENTITLEMENT_GRANT => true,
        _ => null
    };

    public static explicit operator CreateEventSubSubscriptionRequestData(NewEventSubSubscription subscription)
        => new()
        {
            Type = subscription.Type.Name,
            Version = subscription.Type.Version,
            Condition = subscription.Type.Condition,
            Transport = subscription.Transport
        };
}
