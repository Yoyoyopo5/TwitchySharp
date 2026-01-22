using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Deletes an EventSub subscription.
/// </summary>
/// <remarks>
/// <para>
/// Requires an app access token if you use the <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/> as the subscription's transport.
/// <br/>
/// Requires a user access token if you use the <see cref="WebsocketSubscriptionTransport"/>, as the subscription's transport. No particular <see cref="Scope"/> is required.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-eventsub-subscription">Delete EventSub Subscription</see> for more information.
/// </remarks>
public record DeleteEventSubSubscriptionRequest
    : TwitchHelixRequest<DeleteEventSubSubscriptionResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">
    /// If using <see cref="WebhookSubscriptionTransport"/> or <see cref="ConduitSubscriptionTransport"/>, an app access token. 
    /// If using <see cref="WebsocketSubscriptionTransport"/>, a user access token.
    /// </param>
    /// <param name="parameters">The request parameters.</param>
    public DeleteEventSubSubscriptionRequest(
        ClientId clientId, 
        AccessToken accessToken,
        DeleteEventSubSubscriptionRequestParameters parameters
        )
        : base(
            "/eventsub/subscriptions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("id", parameters.SubscriptionId)
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="DeleteEventSubSubscriptionRequest"/>.
/// </summary>
public record DeleteEventSubSubscriptionRequestParameters
{
    /// <summary>
    /// The id of the subscription to delete.
    /// </summary>
    public required EventSubSubscriptionId SubscriptionId { get; set; }
}
